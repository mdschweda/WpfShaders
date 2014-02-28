sampler2D base : register(s0);
sampler2D blend : register(s1);

float amount : register(C0);

inline void hardmix(float baseCh, inout float blendCh) {
    blendCh = blendCh < 1 - baseCh ? 0 : 1;
}

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 cbase = tex2D(base, uv), cblend = tex2D(blend, uv);
    hardmix(cbase.r, cblend.r);
    hardmix(cbase.g, cblend.g);
    hardmix(cbase.b, cblend.b);
    hardmix(cbase.a, cblend.a);

    cbase.rgb *= 1 - amount;
    cblend.rgb *= amount;
    return clamp(cbase + cblend, 0, 1);
}