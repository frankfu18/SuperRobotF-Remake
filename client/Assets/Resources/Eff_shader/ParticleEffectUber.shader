Shader "Custom/ParticleEffect/ParticleEffectUber"
{
    Properties
    {
    	//--------------------------------------------------------------------------------------
		//Base Setting
    	[Space(20)]
        [Header(Base Setting ________________________)]
        [Space(5)]
    	_BlendModeSrc("Source Blend Mode", Float) = 5
		_BlendModeDst("Destination Blend Mode", Float) = 10
		_BlendModeSrc1("Source1 Blend Mode", Float) = 5
		_BlendModeDst1("Destination1 Blend Mode", Float) = 10
		_ZWrite("ZWrite", Float) = 0
		_ZTest("ZTest", Float) = 4
		_CullMode("Cull Mode", Float) = 0
    	
        //--------------------------------------------------------------------------------------
    	// MainTex Mode
    	[Space(20)]
        [Header(MainTex ________________________)]
        [Space(5)]
    	[Toggle(_MAINTEXMODE_ON)] _MainTexMode ("主贴图开关", int) = 0
        [MainTexture]_MainTex("主贴图", 2D) = "white" {}
    	[Toggle]_MainTexClamp("设置Clamp", int) = 0
    	[HDR]_MainTexCol("主贴图颜色", COLOR) = (1,1,1,1)
    	_MainTexLightness("主贴图提取亮度", Range(1,10)) = 1
        _MainTexContrast("主贴图对比度", Range(0,2)) = 0
        _MainTexAnimParam("主贴图UV流动参数", Vector) = (0,0,0,0)
    	[Toggle]_CustomDataMainUV("使用曲线主贴图UV", int) = 0
    	
    	[Toggle(_NORMALTEXMODE_ON)] _NormalTexMode ("法线图开关", int) = 0
    	_NormalTex("法线图", 2D) = "white" {}
    	_Gloss("高光度", Range(0.01,2)) = 0.01
    	[HDR]_SpecularColor("高光颜色", COLOR) = (1,1,1,1)
    	_NormalTexAnimParam("法线图UV流动参数", Vector) = (0,0,0,0)
    	_NormalNoiseTex("法线噪声图", 2D) = "white" {}
    	_NormalTexNoiseAnimParam("法线噪声图UV流动参数", Vector) = (0,0,0,0)

    	//--------------------------------------------------------------------------------------
		//Mask Mode
    	[Space(20)]
        [Header(Mask ________________________)]
        [Space(5)]
    	[Toggle(_MASKMODE_ON)] _MaskMode ("遮罩开关", int) = 0
    	_MaskTex1("遮罩贴图1", 2D) = "white" {}
    	[Toggle]_MaskRA1("遮罩贴图1RA通道切换", int) = 0
		_MaskAnimParam1("遮罩图UV流动参数1", Vector) = (0,0,0,0)
    	[Toggle]_CustomDataMask1UV("使用曲线遮罩图1UV", int) = 0
    	[Space(20)]
    	_MaskTex2("遮罩贴图2", 2D) = "white" {}
    	[Toggle]_MaskRA2("遮罩贴图2RA通道切换", int) = 0
		_MaskAnimParam2("遮罩图UV流动参数2", Vector) = (0,0,0,0)
	    _MaskTex3("遮罩贴图3", 2D) = "white" {}
    	[Toggle]_MaskRA3("遮罩贴图3RA通道切换", int) = 0
		_MaskAnimParam3("遮罩图UV流动参数3", Vector) = (0,0,0,0)
    	
    	//--------------------------------------------------------------------------------------
    	//Distortion Mode
    	[Space(20)]
        [Header(Distortion ________________________)]
        [Space(5)]
    	[Toggle(_DISTORTIONMODE_ON)]_DistortionMode("DistortionMode", int) = 0
    	_DistortionTex("扰动贴图", 2D) = "black" {}
    	_DistortionIntensity("强度", Range(0,0.2)) = 0
//    	_DistortionPow("幂强度", Range(0.01,8)) = 1
        _DistortionAnimParam("扰动UV流动参数", Vector) = (0,0,0,0)
    	[Toggle]_DistortMainTex("扰动主贴图", int) = 0
    	[Toggle]_DistortDissolve("扰动溶解图", int) = 0
    	[Toggle]_CustomDataDistortUV("使用曲线扰动UV", int) = 0
    	
    	//--------------------------------------------------------------------------------------
		//Dissolve Mode
    	[Space(20)]
        [Header(Dissolve ________________________)]
        [Space(5)]
    	[Toggle(_DISSOLVEMODE_ON)]_DissolveMode ("溶解效果开关", int) = 0
        _DissolveTex("溶解贴图",2D) = "white" {}
        _DissolveProgress("溶解度",Range(0,3)) = 0
    	[Toggle]_CustomDataDissolve("使用曲线溶解度", int) = 0
    	[Toggle]_CustomDataDissolveUV("使用曲线扰动溶解UV", int) = 0
        [HDR]_EdgeColor("边缘颜色",Color) = (0,1,0,1)
        _EdgeWidth("边缘宽度",Range(0,2)) = 0.2
        _EdgeColorIntensity("边缘颜色强度",Range(1,2)) = 1 
        _Spread("边缘的扩散值",Range(0,1)) = 0.3
        _SoftNess("柔和",Range(0,0.5)) = 0.4
    	[Toggle]_DissolveRA("溶解贴图RA通道切换", int) = 0
    	[Toggle(_DISSOLVEDIR_ON)]_IsDirDissolve ("方向溶解", int) = 0
    	_DissolveDir("溶解方向", Vector) = (0,0,0,0)
    	_DissolveAnimParam("溶解UV流动参数", Vector) = (0,0,0,0)
    	
        //--------------------------------------------------------------------------------------
		//VertexOffset Mode
    	/*
    	[Space(20)]
        [Header(Vertex Offset ________________________)]
        [Space(5)]
    	[Toggle(_VERTEXOFFSETMODE_ON)]_VertexOffsetMode("VertexOffsetMode", int) = 0
    	[Toggle(_VERTEXOFFSETNORMALMODE_ON)]_VertexOffsetNormalMode("VertexOffsetNormalMode", int) = 0
    	
	    _VertexOffset("VertexOffset : xzy为各方向值,w为通用值", Vector) = (0,0,0,0)
        _VertexOffsetTex("VertexOffsetTex", 2D) = "white" {}
//    	[Toggle]_AlphaMul("AlphaMul", int) = 0
//    	_AlphaTex_OffsetX("AlphaTexOffsetX", Range(-1,1)) = 0
//		_AlphaTex_OffsetY("AlphaTexOffsetY", Range(-1,1)) = 0
//    	_AlphaIntensity("Intensity", Range(0.0,10)) = 1
		_VertexOffsetPow("Pow", Range(0.01,8)) = 1
    	
    	[Toggle]_VertexOffsetAnim("顶点UV动画", int) = 0
    	_VertexOffsetAnimVector("VertexOffsetAnimVector", Vector) = (0,0,0,0)
    	*/
    	
    	//--------------------------------------------------------------------------------------
    	//Fresnel Mode
        [Space(20)]
        [Header(Fresnel ________________________)]
        [Space(5)]
    	[Toggle(_FRESNELMODE_ON)]_FresnelMode("FresnelMode", int) = 0
    	[Toggle]_FresnelEmpty("中空", Int) = 0
    	[Toggle]_FresnelInvert("反转", Int) = 0
		_FresnelRange("范围", Range(0,5)) = 1
		_FresnelIntensity("强度", Range(0,1)) = 0.1
		[HDR]_FresnelCol("颜色", COLOR) = (1,1,1,1)
//		_FresnelAdd("FresnelAdd",Range(0.0,1)) = 0
    	
    	//--------------------------------------------------------------------------------------
    	//RampColor Mode
    	[Space(20)]
        [Header(RampColor ________________________)]
        [Space(5)]
    	[Toggle(_RAMPCOLORMODE_ON)]_RampColorMode("RampColorMode", int) = 0
    	_RampTex("Ramp贴图",2D) = "white" {}
    	_RampRange("Ramp范围", Range(0.0,1)) = 0
		[HDR]_RampColor("Ramp颜色", COLOR) = (1,1,1)
    	
    	//--------------------------------------------------------------------------------------
    	//Soft Mode
    	[Space(20)]
        [Header(SoftMode ________________________)]
        [Space(5)]
    	[Toggle(_SOFTMODE_ON)]_SoftMode("SoftMode", int) = 0
    	[Toggle]_SoftInvert("反转", Int) = 0
		_SoftPow("强度", Range(1,5)) = 1
    	
    	//--------------------------------------------------------------------------------------
    	//Fog Mode
//    	[Space(20)]
//        [Header(FogMode ________________________)]
//        [Space(5)]
//    	[Toggle(_FOG_ON)]_FogMode("雾效", int) = 0
//    	_FogIntensity("强度", Range(0,3000)) = 50
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent+10" "RenderPipeline" = "UniversalPipeline" "IgnoreProjector" = "True"}
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        ColorMask [_ColorMask]
        Pass
        {
        	name "EffectUberPass"
        	Tags{"LightMode" = "Universal2D"}

            Blend [_BlendModeSrc][_BlendModeDst],[_BlendModeSrc1][_BlendModeDst1]
            ZWrite[_ZWrite]
			Cull[_CullMode]
			ZTest[_ZTest]

            HLSLPROGRAM

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 3.0

			#pragma shader_feature_local _MAINTEXMODE_ON
            #pragma shader_feature_local _NORMALTEXMODE_ON
   
            #pragma shader_feature_local _MASKMODE_ON
   
			#pragma shader_feature_local _DISSOLVEMODE_ON
            #pragma shader_feature_local _DISSOLVEDIR_ON
   
            #pragma shader_feature_local _FRESNELMODE_ON
   
            #pragma shader_feature_local _DISTORTIONMODE_ON
   
            #pragma shader_feature_local _RAMPCOLORMODE_ON
   
            // #pragma shader_feature_local _VERTEXOFFSETMODE_ON
            // #pragma shader_feature_local _VERTEXOFFSETNORMALMODE_ON
   
            #pragma shader_feature_local _SOFTMODE_ON

			//#pragma shader_feature_local _FOG_ON

			// //#pragma multi_compile_fog;
			// //#define FOG_LINEAR
			// //#define FOG_EXP2
            
            #pragma vertex vert
            #pragma fragment frag

            
            
            #include "ParticleEffectUber.hlsl"
            
            ENDHLSL
        }
    }
	CustomEditor "ParticleEffectUberGUI"
}