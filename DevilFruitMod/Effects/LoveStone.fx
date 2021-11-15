sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;

float4 StoneEffect(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
	//v ranges from .25-.75
	float v = max(max(color.r, color.g), color.b);
    v = .5 * v + .25;
    color.rgb = float3(v, v, v);
    return color * sampleColor * color.a;
}

technique Technique1
{
	pass StoneEffect
	{
		PixelShader = compile ps_2_0 StoneEffect();
	}
}