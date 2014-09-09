sampler2D input : register(s0);

float contrast : register(c0);
float brightness : register(c1);

float4 main(float2 uv : TEXCOORD) : COLOR { 
    float4 color = tex2D(input, uv);
    if (contrast > 0)
        for (int i = 0; i < 3; i++) {
            if (color[i] >= 0.5)
                color[i] += color[i] * contrast;
            else
                color[i] -= color[i] * contrast;
        }
    else if (contrast < 0)
        for (int i = 0; i < 3; i++)
            color[i] = color[i] + (color[i] - 0.5) * contrast;
    
    color.rgb += brightness;
    return saturate(color);
}
