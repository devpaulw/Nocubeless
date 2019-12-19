float4x4 World;
float4x4 View;
float4x4 Projection;

float3 CubeColor;
float CubeAlpha;

struct VertexShaderInput
{
	float4 Position : POSITION0;
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
    
    float3 color = diffuseColor * diffuseIntensity * lightIntensity + CubeColor;
    
    return saturate(float4(color, CubeAlpha));
}

technique Color
{
	pass Pass1
	{
        AlphaBlendEnable = TRUE;
        DestBlend = INVSRCALPHA;
        SrcBlend = SRCALPHA;
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 PixelShaderFunction();
	}
}