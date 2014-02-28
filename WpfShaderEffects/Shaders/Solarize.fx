sampler2D input : register(s0);

float threshold : register(c0);

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 inverted = tex2D(input, uv);
    for (int i = 0; i < 3; i++)
        if (inverted[i] > threshold)
            inverted[i] = 1 - inverted[i];
    return inverted;
}