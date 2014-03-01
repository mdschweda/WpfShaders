// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

sampler2D base : register(s0);
sampler2D blend : register(s1);

float amount : register(c0);

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 cbase = tex2D(base, uv), cblend;
    cblend = abs(tex2D(base, uv)
        + tex2D(blend, uv)
        - 2 * tex2D(base, uv) * tex2D(blend, uv));
 
    cbase.rgb = saturate(lerp(cbase.rgb, cblend.rgb, amount));
    return cbase;
}