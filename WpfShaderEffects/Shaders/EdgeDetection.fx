// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

sampler2D input : register(s0);

float4 ddxDdy : register(c0);
// static const float3x3 kernel = {
//     -1, -1, -1,
//     -1,  8, -1,
//     -1, -1, -1
// };

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 acc = tex2D(input, uv);
    acc.rgb *= 8.0;

    for (int y = -1; y <= 1; y++)
        for (int x = -1; x <= 1; x++)
            if (x != 0 || y != 0)
                acc.rgb += tex2D(input, saturate(uv + float2(ddxDdy.x * x, ddxDdy.w * y))).rgb * -1.0;

    return saturate(acc);
}