sampler2D input : register(s0);

float4 color : register(c0);

//  CIE 1931 luminance
float luminance(float3 c) {
    // Gamma expansion (sRgb)
    for (int i = 0; i < 3; i++)
        c[i] = c[i] <= 0.04045 ?
            c[i] / 12.92 : pow((c[i] + 0.055) / 1.055, 2.4);
    return dot(float3(0.2126, 0.7152, 0.0722), c.rgb);
}

// Gamma compression (sRgb)
float3 compress(float y) {
    return y <= 0.0031308 ? 12.92 * y : 1.055 * pow(y, 1/2.4) - 0.055;
}

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 c = tex2D(input, uv);
    float3 y = luminance(c.rgb);
    c.rgb = color.rgb * compress(y);
    return c;
}