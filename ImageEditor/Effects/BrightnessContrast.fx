sampler2D input : register(s0);

float brightness : register(c0);
float contrast : register(c1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);
	
	float4 result;
	result.rgb = color.rgb;
	result.rgb = (result.rgb - 0.5f) * max(contrast, 0.0f) + 0.5f;
	result.rgb += brightness;
	
	result.rgb *= color.a;
	result.a = color.a;

	return result;
}