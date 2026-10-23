#define ANGLE2RADIAN	0.01745333f

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

half2 Rotation(half2 uv, half start, half speed)
{
	// if(abs(start) >= 0.1h || abs(speed) >= 0.1h)
	// {
		half2 center = half2(0.5h, 0.5h);
		half2 verOffRotateUV = uv.xy - center;
		float verOffDegree = start * ANGLE2RADIAN;
		float verOffSpeed = speed * _Time.y;
		verOffRotateUV = half2(verOffRotateUV.x * cos(verOffDegree + verOffSpeed) - verOffRotateUV.y * sin(verOffDegree + verOffSpeed),
			verOffRotateUV.x * sin(verOffDegree + verOffSpeed) + verOffRotateUV.y * cos(verOffDegree + verOffSpeed));
		verOffRotateUV += center;
		uv = verOffRotateUV;
	// }
	return uv;
}

float remap(float x,float oldmin,float oldMax,float newMin,float newMax)
{
	return (x - oldmin)/(oldMax - oldmin) * (newMax-newMin) + newMin;   
}

// float3 FlowUVW (float2 uv, float2 flowVector, float time) {
// 	float progress = frac(time);
// 	float3 uvw;
// 	uvw.xy = uv - flowVector * progress;
// 	uvw.z = 1;
// 	return uvw;
// }

float3 FlowUVW (float2 uv, float2 flowVector, float time, bool flowB) {
	float phaseOffset = flowB ? 0.5 : 0;
	float progress = frac(time + phaseOffset);
	float3 uvw;
	uvw.xy = uv - flowVector * progress;
	uvw.z = 1 - abs(1 - 2 * progress);
	return uvw;
}

// float3 FlowUVW (float2 uv, float2 flowVector, float2 jump,float flowOffset, float tiling, float time, bool flowB)
// {
// 	float phaseOffset = flowB ? 0.5 : 0;
// 	float progress = frac(time + phaseOffset);
// 	float3 uvw;
// 	uvw.xy = uv - flowVector * (progress + flowOffset);
// 	uvw.xy *= tiling;
// 	uvw.xy += phaseOffset;
// 	uvw.xy += (time - progress) * jump;
// 	uvw.z = 1 - abs(1 - 2 * progress);
// 	return uvw;
// }

half3 BlinnPhongLight(half3 n, half3 v, half3 l, half gloss)
{
	half3 H = normalize(l + v);
	return pow(max(0, dot(n, H)), gloss);
}

half3 NormalScale(half3 n, half nScale)
{
	half3 normal;
	normal.xy = n.xy * nScale;
	normal.z = sqrt(1.0 - saturate(dot(normal.xy, normal.xy)));
	return normal;
}