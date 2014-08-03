sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);
	float4 result;

	result.rgb = 1.0f - color.rgb;
	result.rgb *= color.a;
	result.a = color.a;
	
	return result;
}