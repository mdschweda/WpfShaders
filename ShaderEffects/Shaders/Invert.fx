// Copyright (c) 2014 Marcus Schweda
// This file is licensed under the MIT license (see LICENSE)

sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 inverted = tex2D(input, uv);
    inverted.rgb = 1 - inverted.rgb;
    return inverted;
}