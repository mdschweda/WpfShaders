sampler2D input : register(s0);

float gamma : register(C0);

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 c = tex2D(input, uv);
    return saturate(float4(pow(c.rgb, 1 / gamma), c.a));
}