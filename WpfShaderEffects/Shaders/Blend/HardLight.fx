sampler2D base : register(s0);
sampler2D blend : register(s1);

float amount : register(C0);

inline void hardlight(float baseCh, inout float blendCh) {
    blendCh = blendCh < 0.5 ?
        // Dunkel: Doppeltes Multiply
        2 * baseCh * blendCh :
        // Hell: Doppeltes Screen
        1 - 2 * (1 - baseCh) * (1 - blendCh);
}

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 cbase = tex2D(base, uv), cblend = tex2D(blend, uv);
    hardlight(cbase.r, cblend.r);
    hardlight(cbase.g, cblend.g);
    hardlight(cbase.b, cblend.b);
    hardlight(cbase.a, cblend.a);

    cbase.rgb *= 1 - amount;
    cblend.rgb *= amount;
    return clamp(cbase + cblend, 0, 1);
}