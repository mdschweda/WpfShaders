// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

sampler2D input : register(s0);

float2 shiftR : register(c0);
float2 shiftG : register(c1);
float2 shiftB : register(c2);
float4 ddxDdy : register(c3);

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 c = float4(0, 0, 0, 1);

    // Red
    float2 uv2 = float2(uv.x - ddxDdy.x * shiftR.x, uv.y - ddxDdy.w * shiftR.y);
    if (uv.x >= 0 && uv2.x <= 1.0 && uv2.y >= 0 && uv2.y <= 1.0)
        c.r = tex2D(input, uv2).r;
    // Green
    uv2 = float2(uv.x - ddxDdy.x * shiftG.x, uv.y - ddxDdy.w * shiftG.y);
    if (uv.x >= 0 && uv2.x <= 1.0 && uv2.y >= 0 && uv2.y <= 1.0)
        c.g = tex2D(input, uv2).g;
    // Blue
    uv2 = float2(uv.x - ddxDdy.x * shiftB.x, uv.y - ddxDdy.w * shiftB.y);
    if (uv.x >= 0 && uv2.x <= 1.0 && uv2.y >= 0 && uv2.y <= 1.0)
        c.b = tex2D(input, uv2).b;

    return c;
}