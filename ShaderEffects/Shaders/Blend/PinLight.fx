// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

sampler2D base : register(s0);
sampler2D blend : register(s1);

float amount : register(c0);

inline void pinlight(float baseCh, inout float blendCh) {
    if (baseCh < 2 * blendCh - 1)
        blendCh = 2 * blendCh - 1;
    else if (baseCh > 2 * blendCh)
        blendCh = 2 * blendCh;
    else
        blendCh = baseCh;
}

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 cbase = tex2D(base, uv), cblend = tex2D(blend, uv);
    pinlight(cbase.r, cblend.r);
    pinlight(cbase.g, cblend.g);
    pinlight(cbase.b, cblend.b);
    pinlight(cbase.a, cblend.a);

    cbase.rgb = saturate(lerp(cbase.rgb, cblend.rgb, amount));
    return cbase;
}