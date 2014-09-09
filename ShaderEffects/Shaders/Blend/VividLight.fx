// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

sampler2D base : register(s0);
sampler2D blend : register(s1);

float amount : register(c0);

inline void vividlight(float baseCh, inout float blendCh) {
    blendCh = blendCh < 0.5 ?
        1 - (1 - baseCh) / (2 * blendCh) :
        baseCh / (2 * (1 - blendCh));
}

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 cbase = tex2D(base, uv), cblend = tex2D(blend, uv);
    vividlight(cbase.r, cblend.r);
    vividlight(cbase.g, cblend.g);
    vividlight(cbase.b, cblend.b);
    vividlight(cbase.a, cblend.a);

    cbase.rgb = saturate(lerp(cbase.rgb, cblend.rgb, amount));
    return cbase;
}