// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

sampler2D input : register(s0);

float4 ddxDdy : register(c0);
// static const float3x3 kernel = {
//      0, -1,  0,
//     -1,  5, -1,
//      0, -1,  0
// };

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 acc = tex2D(input, uv);
    acc.rgb *= 5.0;
    acc.rgb += -1.0 * tex2D(input, saturate(uv + float2(ddxDdy.x, 0.0))).rgb;
    acc.rgb += -1.0 * tex2D(input, saturate(uv - float2(ddxDdy.x, 0.0))).rgb;
    acc.rgb += -1.0 * tex2D(input, saturate(uv + float2(0.0, ddxDdy.w))).rgb;
    acc.rgb += -1.0 * tex2D(input, saturate(uv - float2(0.0, ddxDdy.w))).rgb;

    return saturate(acc);
}