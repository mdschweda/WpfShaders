sampler2D base : register(s0);
sampler2D blend : register(s1);

float amount : register(C0);

float4 main(float2 uv : TEXCOORD) : COLOR {
    float4 cbase = tex2D(base, uv), cblend;
    cblend = dot(tex2D(base, uv), 1) > dot(tex2D(blend, uv), 1) ?
    	tex2D(blend, uv) : tex2D(base, uv);
 
    cbase.rgb *= 1 - amount;
    cblend.rgb *= amount;
    return saturate(cbase + cblend);
}