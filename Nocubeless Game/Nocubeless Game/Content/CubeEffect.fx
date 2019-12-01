float4x4 World;
float4x4 View;
float4x4 Projection;

float4 AmbientColor; // to make a float3, and is a Cube Color

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float4 Normal : NORMAL0;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
    float4 PositionWorld : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
    output.PositionWorld = worldPosition;
	
	//float diffuseIntensity = 1.0;
	////float4 lightDirection = float4(-0.66, 1.0, 0.33, 1.0);
    
	
 //   float3 normal = normalize(cross(ddy(worldPosition.xyz), ddx(worldPosition.xyz)));
	
	//float lightIntensity = dot(lightDirection.rgb, normal);

	//output.Color = diffuseColor * diffuseIntensity * lightIntensity;

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    const float3 lightDirection = float3(-0.66, 1.0, 0.33);
    const float3 diffuseColor = float3(0.2, 0.2, 0.2);
    const float diffuseIntensity = 1.0;
    
    float3 normal = cross(ddx(input.PositionWorld.xyz), ddy(input.PositionWorld.xyz));
    normal = normalize(normal);
    
    float lightIntensity = dot(lightDirection, normal);
    
    float3 lightColor = diffuseColor * diffuseIntensity * lightIntensity + AmbientColor.rgb;
    
    return saturate(float4(lightColor, 1.0));
}

technique Color
{
	pass Pass1
	{
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 PixelShaderFunction();
	}
}