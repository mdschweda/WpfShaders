sampler2D base : register(s0);
sampler2D blend : register(s1);

float amount : register(C0);

inline void softlight(float baseCh, inout float blendCh) {
    blendCh = blendCh < 0.5 ?
        2 * baseCh * blendCh + baseCh * baseCh * (1 - 2 * blendCh) :
        2 * baseCh * (1 - blendCh) + sqrt(baseCh) * (2 * blendCh - 1);
}

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 cbase = tex2D(base, uv), cblend = tex2D(blend, uv);
    softlight(cbase.r, cblend.r);
    softlight(cbase.g, cblend.g);
    softlight(cbase.b, cblend.b);
    softlight(cbase.a, cblend.a);

    cbase.rgb *= 1 - amount;
    cblend.rgb *= amount;
    return clamp(cbase + cblend, 0, 1);
}