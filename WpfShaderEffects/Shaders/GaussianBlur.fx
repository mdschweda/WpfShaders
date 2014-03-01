// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

sampler2D input : register(s0);

float sigma : register(c0);
float4 ddxDdy : register(c1);

static const float pi = 3.1415926536;

inline float3 px(float2 uv : TEXCOORD, float x, float y, float coeff) {
    float2 pos = float2(uv.x + ddxDdy.x * x, uv.y + ddxDdy.w * y);
    // Confine convolution within image boundaries
    saturate(pos);
    return tex2D(input, pos).rgb * coeff;
}

float4 main(float2 uv : TEXCOORD) : COLOR {
    float3 acc;
    // Gaussian curve height
    float norm = 1 / (2 * pi * sigma * sigma);

    // 48x48 kernel
    for (int y = 0; y < 24; y++) {
        for (int x = 0; x < 24; x++) {
            float coeff = exp(-(x * x + y * y) / (2.0 * sigma * sigma));
            acc += px(uv, x, y);
            // Kernel symmetry
            if (x != 0 && y != 0)
                acc += px(uv, -x, -y);
            if (y != 0)
                acc += px(uv, x, -y);
            if (x != 0)
                acc += px(uv, -x, y);
        }
    }

    acc = saturate(acc * norm);
    return float4(acc.r, acc.g, acc.b, tex2D(input, uv).a);
}