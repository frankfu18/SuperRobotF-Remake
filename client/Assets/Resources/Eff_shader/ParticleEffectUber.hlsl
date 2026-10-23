#include "ParticleEffectUberTool.hlsl"
//#include "ParticleEffectUberTool.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

//CBUFFER_START(UnityPerMaterial)

#if _MAINTEXMODE_ON
	TEXTURE2D(_MainTex);    SAMPLER(sampler_MainTex);
	int _MainTexClamp;
	half4 _MainTex_ST;
	half4 _MainTexCol;
	half _MainTexContrast;
	half _MainTexLightness;
	half4 _MainTexAnimParam;
	int _CustomDataMainUV;

	int _DistortMainTex;

	#if _NORMALTEXMODE_ON
		TEXTURE2D(_NormalTex);    SAMPLER(sampler_NormalTex);
		half4 _NormalTex_ST;
		half _Gloss;
		half4 _SpecularColor;
		half4 _NormalTexAnimParam;
		TEXTURE2D(_NormalNoiseTex);    SAMPLER(sampler_NormalNoiseTex);
		half4 _NormalNoiseTex_ST;
		half4 _NormalTexNoiseAnimParam;
	#endif

#endif

#if _MASKMODE_ON
	TEXTURE2D(_MaskTex1);    SAMPLER(sampler_MaskTex1);
	TEXTURE2D(_MaskTex2);    SAMPLER(sampler_MaskTex2);
	TEXTURE2D(_MaskTex3);    SAMPLER(sampler_MaskTex3);

	half4 _MaskTex1_ST;
	half4 _MaskTex2_ST;
	half4 _MaskTex3_ST;

	int _MaskRA1;
	int _MaskRA2;
	int _MaskRA3;

	half4 _MaskAnimParam1;
	half4 _MaskAnimParam2;
	half4 _MaskAnimParam3;
 
	int _CustomDataMask1UV;
#endif

#if _DISSOLVEMODE_ON
	TEXTURE2D(_DissolveTex);    SAMPLER(sampler_DissolveTex);
	half4 _DissolveTex_ST;
	half _DissolveProgress;
	int _CustomDataDissolve;
	int _CustomDataDissolveUV;
	half4 _EdgeColor;
	half _EdgeWidth;
	half _EdgeColorIntensity;
	// half _EdgeSmoothness;
	half _Spread;
	half _SoftNess;
	int _DistortDissolve;
	int _DissolveRA;
	// int _IsCenterDissolve;
	int _IsDirDissolve;
	half4 _DissolveDir;
	half4 _DissolveAnimParam;
#endif

#if _FRESNELMODE_ON
	int _FresnelEmpty;
	int _FresnelInvert;
	half _FresnelRange;
	half _FresnelIntensity;
	half4 _FresnelCol;
#endif

#if _DISTORTIONMODE_ON
	TEXTURE2D(_DistortionTex);    SAMPLER(sampler_DistortionTex);
	half4 _DistortionTex_ST;
	half _DistortionIntensity;
	half4 _DistortionAnimParam;
	int _CustomDataDistortUV;
#endif

#if _VERTEXOFFSETMODE_ON
	TEXTURE2D(_VertexOffsetTex);    SAMPLER(sampler_VertexOffsetTex);
	half4 _VertexOffsetTex_ST;
	half4 _VertexOffset;
	half _VertexOffsetPow;
	int _VertexOffsetAnim;
	half4 _VertexOffsetAnimVector;
#endif

#if _RAMPCOLORMODE_ON
	TEXTURE2D(_RampTex);    SAMPLER(sampler_RampTex);
	half _RampRange;
	half3 _RampColor;
#endif

#if _SOFTMODE_ON
	int _SoftInvert;
	half _SoftPow;
	TEXTURE2D_X(_CameraDepthTexture);    SAMPLER(sampler_CameraDepthTexture);
#endif

// #if _FOG_ON
// 	half _FogIntensity;
// #endif

// CBUFFER_END

//-------------------------------------------------------------------------------------
// Input Struct

struct appdata
{
	half4 uv : TEXCOORD0;
	half4 uv1 : TEXCOORD1;
	half2 uv2 : TEXCOORD2;
	float4 vertex : POSITION;
	half4 col : COLOR0;

	#if _FRESNELMODE_ON
		half3 normal : NORMAL;
	#endif

};

struct v2f
{
	float4 posCS : SV_POSITION;
	// half4 uv_main_ori : TEXCOORD0;
	half4 uv : TEXCOORD0;
	half4 customData1 : TEXCOORD10;
	half4 customData2 : TEXCOORD11;

	// // half fogCoord : TEXCOORD12;
	// // half4 fogInfo : TEXCOORD12;

	half4 col : COLOR0;

	#if _NORMALTEXMODE_ON || _FRESNELMODE_ON || _RAMPCOLORMODE_ON || _DISSOLVEMODE_ON
		half3 worldNormal : NORMAL;
	#endif

	#if _NORMALTEXMODE_ON
		half4 uv_normalTex : TEXCOORD1;
	#endif

	#if _DISSOLVEMODE_ON
		half2 uv_dissolve : TEXCOORD2;
		half dissolveDirFactor : TEXCOORD3;
	#endif

	#if _MASKMODE_ON
		half2 uv_mask1 : TEXCOORD4;
		half2 uv_mask2 : TEXCOORD5;
		half2 uv_mask3 : TEXCOORD6;
	#endif

	#if _FRESNELMODE_ON || _NORMALTEXMODE_ON //|| _FOG_ON
		half3 worldPos : TEXCOORD7;
	#endif
		
	// #if _NORMALTEXMODE_ON
	// 	half2 uv_Normal : TEXCOORD7;
	// #endif

	#if _DISTORTIONMODE_ON
		half2 uv_distortion : TEXCOORD8;
	#endif

	#if _SOFTMODE_ON
		half4 projPos : TEXCOORD9;
	#endif
};

//-------------------------------------------------------------------------------------
// Func

v2f vert(appdata v)
{
	v2f o = (v2f)0;

	o.customData1.xy = v.uv.zw;
	o.customData1.zw = v.uv1.xy;
	o.customData2.xy = v.uv1.zw;
	o.customData2.zw = v.uv2;

	o.col = v.col;

	/*
	#if _VERTEXOFFSETMODE_ON
		half3 vOffset;
		half2 uv_VertexOffset = v.uv.xy;
		uv_VertexOffset = uv_VertexOffset * _VertexOffsetTex_ST.xy + _VertexOffsetTex_ST.zw;
		uv_VertexOffset = saturate(uv_VertexOffset);

		uv_VertexOffset.xy = Rotation(uv_VertexOffset.xy, _VertexOffsetAnimVector.z, _VertexOffsetAnimVector.w) * _VertexOffsetAnim + uv_VertexOffset.xy*(1 - _VertexOffsetAnim);
		uv_VertexOffset.xy += _Time.yy * _VertexOffsetAnimVector.xy * _VertexOffsetAnim;

		half3 verOffCol = SAMPLE_TEXTURE2D_LOD(_VertexOffsetTex, sampler_VertexOffsetTex, uv_VertexOffset,0);
		
		#if _VERTEXOFFSETNORMALMODE_ON
			verOffCol = pow(abs(verOffCol), _VertexOffsetPow);
			// #if _CUSTOMUVMODE_ON
			// 	// if(_Unse_WPOTexCustomUV<0.1h)
			// 	// 	vOffset = verOffCol * _VertexOffset.xyz * v.uv1.w * v.col.a;
			// 	// else
			// 		vOffset = verOffCol * _VertexOffset.xyz * _VertexOffset.w * v.col.a;
			// #elif !_CUSTOMUVMODE_ON
				vOffset = verOffCol * _VertexOffset.xyz * _VertexOffset.w * v.col.a;
			// #endif
	
			vOffset = vOffset * normalize(v.normal.xyz);
	
		#else
			// #if _CUSTOMUVMODE_ON
			// 	// if(_Unse_WPOTexCustomUV<0.1h)
			// 	// 	vOffset = verOffCol * _VertexOffset.xyz * v.uv1.w * v.col.a;
			// 	// else
			// 		vOffset = verOffCol * _VertexOffset.xyz * _VertexOffset.w * v.col.a;
			// #elif !_CUSTOMUVMODE_ON
				vOffset = verOffCol * _VertexOffset.xyz * _VertexOffset.w; //* v.col.a;
			// #endif

		#endif

		v.vertex.xyz += vOffset;
	
	#endif
	*/

	o.posCS = TransformObjectToHClip(v.vertex.xyz);

	// // #if _FOG_ON
	// // 	o.fogCoord = ComputeFogFactor(o.posCS.z);
	// // //o.worldPos = TransformObjectToWorld(v.vertex.xyz);
	// // // o.fogInfo = MyFogCore(o.worldPos);
	// // #endif

	#if _MAINTEXMODE_ON
		o.uv.xy = TRANSFORM_TEX(v.uv,_MainTex);
		o.uv.xy += o.customData2.zw * _CustomDataMainUV;
		o.uv.xy = Rotation(o.uv.xy, _MainTexAnimParam.z, _MainTexAnimParam.w);
		o.uv.xy += frac(_Time.x * _MainTexAnimParam.xy);
		#if _NORMALTEXMODE_ON
			o.uv_normalTex.xy = TRANSFORM_TEX(v.uv,_NormalTex);
			// o.uv_normalTex.xy = Rotation(o.uv_normalTex.xy, _NormalTexAnimParam.z, _NormalTexAnimParam.w);
			// o.uv_normalTex.xy += frac(_Time.x * _NormalTexAnimParam.xy);

			o.uv_normalTex.zw = TRANSFORM_TEX(v.uv,_NormalNoiseTex);
			o.uv_normalTex.zw = Rotation(o.uv_normalTex.zw, _NormalTexNoiseAnimParam.z, _NormalTexNoiseAnimParam.w);
			o.uv_normalTex.zw += frac(_Time.x * _NormalTexNoiseAnimParam.xy);
		#endif
		
	#endif
		
	#if _MASKMODE_ON
		o.uv_mask1.xy = TRANSFORM_TEX(v.uv,_MaskTex1);

		o.uv_mask1.xy += half2(o.customData1.x,o.customData1.y)*_CustomDataMask1UV;
		o.uv_mask1.xy = Rotation(o.uv_mask1.xy, _MaskAnimParam1.z, _MaskAnimParam1.w);
		o.uv_mask1.xy += frac(_Time.x * _MaskAnimParam1.xy);

		o.uv_mask2.xy = TRANSFORM_TEX(v.uv,_MaskTex2);
		o.uv_mask2.xy = Rotation(o.uv_mask2.xy, _MaskAnimParam2.z, _MaskAnimParam2.w);
		o.uv_mask2.xy += frac(_Time.x * _MaskAnimParam2.xy);

		o.uv_mask3.xy = TRANSFORM_TEX(v.uv,_MaskTex3);
		o.uv_mask3.xy = Rotation(o.uv_mask3.xy, _MaskAnimParam3.z, _MaskAnimParam3.w);
		o.uv_mask3.xy += frac(_Time.x * _MaskAnimParam3.xy);
	#endif

	#if _DISSOLVEMODE_ON
		o.uv_dissolve = TRANSFORM_TEX(v.uv.xy, _DissolveTex);
		o.uv_dissolve = Rotation(o.uv_dissolve, _DissolveAnimParam.z, _DissolveAnimParam.w);
		o.uv_dissolve += frac(_Time.x * _DissolveAnimParam.xy);
	
		#if _DISSOLVEDIR_ON
			half3 pos = half3(0,0,0) - v.vertex.xyz;
			o.dissolveDirFactor = dot(normalize(_DissolveDir), pos)*3;
		#endif
	#endif

	#if _FRESNELMODE_ON
		o.worldPos = TransformObjectToWorld(v.vertex.xyz);
		o.worldNormal = TransformObjectToWorldNormal(v.normal);
	#endif

	#if _DISTORTIONMODE_ON
		o.uv_distortion.xy = TRANSFORM_TEX(v.uv.xy, _DistortionTex);

		o.uv_distortion.xy += o.customData1.zw * _CustomDataDistortUV;

		o.uv_distortion.xy = Rotation(o.uv_distortion.xy, _DistortionAnimParam.z, _DistortionAnimParam.w);
		o.uv_distortion.xy += frac(_Time.x * _DistortionAnimParam.xy);
	#endif

	#if _SOFTMODE_ON
		o.projPos = ComputeScreenPos(o.posCS);
		o.projPos.z = -mul(UNITY_MATRIX_MV, v.vertex).z;
	#endif

	return o;
}

half4 frag(v2f i, half facing : VFACE) : SV_Target
{
	half4 finalCol = half4(0,0,0,1);
	half distor = 0;
	half mask = 1;

	#if _FRESNELMODE_ON || _NORMALTEXMODE_ON
		float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos);
		half3 lightDir = normalize(TransformObjectToWorldDir(_MainLightPosition.xyz));
	#endif


	#if _MASKMODE_ON
		half4 mask1Col = SAMPLE_TEXTURE2D(_MaskTex1, sampler_MaskTex1, i.uv_mask1);
		half4 mask2Col = SAMPLE_TEXTURE2D(_MaskTex2, sampler_MaskTex2, i.uv_mask2);
		half4 mask3Col = SAMPLE_TEXTURE2D(_MaskTex3, sampler_MaskTex3, i.uv_mask3);
	
		mask = mask1Col.a * _MaskRA1 + mask1Col.r * (1-_MaskRA1);
		mask *= mask2Col.a * _MaskRA2 + mask2Col.r * (1-_MaskRA2);
		mask *= mask3Col.a * _MaskRA3 + mask3Col.r * (1-_MaskRA3);
	#endif

	#if _DISTORTIONMODE_ON
		distor = SAMPLE_TEXTURE2D(_DistortionTex, sampler_DistortionTex, i.uv_distortion.xy).r;
		distor = distor * _DistortionIntensity;
	#endif
	
	#if _MAINTEXMODE_ON
	
		i.uv.xy = saturate(i.uv.xy) * _MainTexClamp + i.uv.xy * (1-_MainTexClamp);
	
		i.uv.xy += distor * _DistortMainTex;
		//i.uv.xy = saturate(i.uv.xy);
		finalCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xy) * _MainTexCol;
		finalCol.a *= mask;
		finalCol.rgb = finalCol.rgb + (finalCol.rgb-0.5) * _MainTexContrast;
		finalCol.a = pow(abs(finalCol.a),_MainTexLightness);
	
	
		#if _NORMALTEXMODE_ON
			i.uv_normalTex.xy += distor * _DistortMainTex;
			i.uv_normalTex.zw += distor * _DistortMainTex;

			half2 normalTexUV = Rotation(i.uv_normalTex.xy, _NormalTexAnimParam.z, _NormalTexAnimParam.w);
			half2 normalTexUV1 = normalTexUV + frac(_Time.x * _NormalTexAnimParam.xy);
			half2 normalTexUV2 = normalTexUV - frac(_Time.x * _NormalTexAnimParam.xy);
			
			half normalNoise = SAMPLE_TEXTURE2D(_NormalNoiseTex,sampler_NormalNoiseTex, i.uv_normalTex.zw).r;
			
			half3 packedNormal1 = UnpackNormal(SAMPLE_TEXTURE2D(_NormalTex,sampler_NormalTex, normalTexUV1)*normalNoise);
			half3 packedNormal2 = UnpackNormal(SAMPLE_TEXTURE2D(_NormalTex,sampler_NormalTex, normalTexUV2)*normalNoise);
			half3 tangentNormal = normalize(packedNormal1 + packedNormal2);
	

			// half normalNoise = SAMPLE_TEXTURE2D(_NormalNoiseTex,sampler_NormalNoiseTex, i.uv.zw);
			// half3 packedNormal = UnpackNormal(SAMPLE_TEXTURE2D(_NormalTex,sampler_NormalTex, i.uv.zw)*normalNoise);
			// half3 tangentNormal = normalize(packedNormal);

	
			//tangentNormal = NormalScale(tangentNormal,1);
			// half3 specular = 	_MainLightColor.rgb * _Specular.rgb * BlinnPhongLight(tangentNormal,viewDir,lightDir,_Gloss);
			half3 specular = _MainLightColor.rgb * _SpecularColor * BlinnPhongLight(tangentNormal,viewDir,lightDir,_Gloss);
			finalCol.rgb += specular;
		#endif
	
	#endif

	#if _DISSOLVEMODE_ON
		i.uv_dissolve.xy += distor * _DistortDissolve;

		half4 dissolveCol = SAMPLE_TEXTURE2D(_DissolveTex, sampler_DissolveTex,i.uv_dissolve);
		
		half dissolve = dissolveCol.a * _DissolveRA + dissolveCol.r * (1-_DissolveRA);
	
		half dissolveProgress = i.customData2.x*_CustomDataDissolve + _DissolveProgress*(1-_CustomDataDissolve);
		half remapData = remap(dissolveProgress,0,1,-_Spread,1);
	
		dissolve = dissolve - remapData + (_EdgeWidth*(_Spread+_SoftNess));
		dissolve /= _Spread;

		dissolve = dissolve + i.dissolveDirFactor; //* _WorldSpaceScale;
	
		half dis = saturate(1 - distance(_SoftNess,dissolve) / _EdgeWidth);

		half dAlpha = smoothstep(_SoftNess,0.5, dissolve);
		
		finalCol.a = dAlpha * finalCol.a;
	
		//clip(finalCol.a - 0.5);
	
		half4 edgeColor = _EdgeColor * _EdgeColorIntensity;
		finalCol.rgb = lerp(finalCol,edgeColor,dis).rgb;

	#endif

	#if _FRESNELMODE_ON
		//float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos);
		i.worldNormal = normalize(i.worldNormal);
		half fresnel =saturate((1-_FresnelInvert) * (1-dot(i.worldNormal, viewDir)) + _FresnelInvert * dot(i.worldNormal, viewDir));
		fresnel = _FresnelIntensity*pow(fresnel,_FresnelRange);
		finalCol.rgb = lerp(finalCol,_FresnelCol,fresnel);
		finalCol.a = finalCol.a*fresnel*_FresnelEmpty + finalCol.a*(1-_FresnelEmpty);
	#endif

	#if _RAMPCOLORMODE_ON
		half3 worldLightDir = normalize(_MainLightPosition.xyz);
		half halfLambert = dot(i.worldNormal, worldLightDir) * 0.5 + 0.5;
		half ramp = SAMPLE_TEXTURE2D(_RampTex, sampler_RampTex, float2(saturate(halfLambert - _RampRange), 0.5)).r;
		finalCol.rgb = lerp(_RampColor,finalCol.rgb, ramp);
	#endif

	#if _SOFTMODE_ON
		float sceneZ = LinearEyeDepth(SAMPLE_TEXTURE2D_X(_CameraDepthTexture, sampler_CameraDepthTexture, i.projPos.xy / i.projPos.w), _ZBufferParams);
		float partZ = i.projPos.w;
		float fade = saturate(sceneZ-partZ);
		fade =  (1 - fade)*_SoftInvert + fade*(1-_SoftInvert);
		finalCol.a *= pow(fade,_SoftPow);
	#endif

	finalCol = finalCol * i.col;

	// #if _FOG_ON
	//
	// 	half disCamera = max(0,distance(i.worldPos,half3(_WorldSpaceCameraPos.x,i.worldPos.y,_WorldSpaceCameraPos.z))-200);
	//
	// 	finalCol.a *= saturate(pow((_FogIntensity-disCamera)/_FogIntensity,3));
	//
	// 	//finalCol.rgb = MixFog(finalCol,i.fogCoord);
	// #endif
	
	return finalCol;
}
