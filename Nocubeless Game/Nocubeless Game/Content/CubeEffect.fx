float4x4 World;
float4x4 View;
float4x4 Projection;
float4 AmbientColor;

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float4 Normal : NORMAL0;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	float4 diffuseColor = float4(0.2, 0.2, 0.2, 1);
	float diffuseIntensity = 1.0;
	float4 lightDirection = float4(-0.66, 1.0, 0.33, 1.0);
	
	//float4 normal = normalize(mul(input.Normal, World));
	
	float lightIntensity = dot(lightDirection, input.Normal);

	output.Color = saturate(diffuseColor * diffuseIntensity * lightIntensity);

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	return saturate(input.Color + AmbientColor);
}

technique Color
{
	pass Pass1
	{
		VertexShader = compile vs_2_0 VertexShaderFunction();
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}