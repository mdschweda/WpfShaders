sampler2D input : register(s0);

float2 shiftR : register(C0);
float2 shiftG : register(C1);
float2 shiftB : register(C2);
float4 ddxDdy : register(C3);

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