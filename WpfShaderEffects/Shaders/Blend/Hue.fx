// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

sampler2D base : register(s0);
sampler2D blend : register(s1);

float amount : register(c0);

static const float3 lumarec601 = float3(0.299, 0.587, 0.114);
static const float3 lumarec709 = float3(0.2126, 0.7152, 0.0722);

// RGB -> Hue/Chroma/Luma
float4 hcy(float4 c, float3 lumacoeff) {
    float4 c2 = c.a;
    // Luma
    c2[2] = dot(lumacoeff, c.rgb);
    // Chroma
    float M = max(c.r, max(c.g, c.b));
    c2[1] = M - min(c.r, min(c.g, c.b)); 
    // Hue
    if (c2[1] != 0) {
        if (M == c.r)
            c2[0] = ((c.g - c.b) / c2[1]) % 6;
        else if (M == c.g)
            c2[0] = (c.b - c.r) / c2[1] + 2;
        else
            c2[0] = (c.r - c.g) / c2[1] + 4;
        if (c2[0] < 0)
            c2[0] += 6;
    } else {
        c2[0] = 0;
    }
    return c2;
}

float4 rgb(float4 c, float3 lumacoeff) {
    float4 c2 = c[3];
    float X = c[1] * (1 - abs(c[0] % 2 - 1));

    if (0 <= c[0] && c[0] < 1)
        c2.rgb = float3(c[1], X, 0);
    else if (1 <= c[0] && c[0] < 2)
        c2.rgb = float3(X, c[1], 0);
    else if (2 <= c[0] && c[0] < 3)
        c2.rgb = float3(0, c[1], X);
    else if (3 <= c[0] && c[0] < 4)
        c2.rgb = float3(0, X, c[1]);
    else if (4 <= c[0] && c[0] < 5)
        c2.rgb = float3(X, 0, c[1]);
    else if (5 <= c[0] && c[0] < 6)
        c2.rgb = float3(c[1], 0, X);

    c2.rgb += c[2] - dot(lumacoeff, c2.rgb);
    return c2;
}

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 cbase = tex2D(base, uv), cblend;
    float4 hcyblend = hcy(cbase, lumarec601);
    hcyblend[0] = hcy(tex2D(blend, uv), lumarec601)[0];
    cblend = float4(rgb(hcyblend, lumarec601).rgb, cbase.a);
 
    cbase.rgb = saturate(lerp(cbase.rgb, cblend.rgb, amount));
    return cbase;
}