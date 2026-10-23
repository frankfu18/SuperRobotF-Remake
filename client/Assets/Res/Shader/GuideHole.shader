Shader "UI/GuideHole"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0

        [Toggle] _IsRect ("IsRect", Float) = 1
        _Params ("Params", Vector) = (0, 0, 100, 100)
        _Gradient ("Gradient", Range(0, 1000)) = 50
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            
            float _IsRect;
            float4 _Params;
            float _Gradient;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                #ifdef UNITY_UI_CLIP_RECT
                    color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                    clip (color.a - 0.001);
                #endif

                // TODO if else step优化
                half val = 1;
                if(_IsRect > 0)
                {
                    half x = _Params.x - _Gradient;
                    half y = _Params.y - _Gradient;
                    half z = _Params.z + _Gradient;
                    half w = _Params.w + _Gradient;
                    half4 outerBound = float4(x, y, z, w);

                    if(UnityGet2DClipping(IN.worldPosition.xy, _Params) == 1)
                    {
                        val = 0;
                    }
                    else if(UnityGet2DClipping(IN.worldPosition.xy, outerBound) == 1)
                    {
                        half width = _Params.z - _Params.x;
                        half height = _Params.w - _Params.y;
                        half innerArea = width * height;
                        half outerArea = (outerBound.z - outerBound.x) * (outerBound.w - outerBound.y);
                        half2 offset = half2(0,0);
                        if(IN.worldPosition.x < _Params.x)
                            offset.x = _Params.x - IN.worldPosition.x;
                        else if(IN.worldPosition.x > _Params.z)
                            offset.x = IN.worldPosition.x - _Params.z;
                        if(IN.worldPosition.y < _Params.y)
                            offset.y = _Params.y - IN.worldPosition.y;
                        else if(IN.worldPosition.y > _Params.w)
                            offset.y = IN.worldPosition.y - _Params.w;
                        half ofst = max(offset.x, offset.y) * 2;
                        val = ((width + ofst) * (height + ofst) - innerArea) / (outerArea - innerArea);
                    }
                }
                else
                {
                    half currentDistance = distance(IN.worldPosition.xy, _Params.xy);
                    half gradientDistance = _Params.z + _Gradient;
                    
                    if(currentDistance <= _Params.z)
                        val = 0;
                    else if(currentDistance <= gradientDistance)
                        val = (currentDistance - _Params.z) / (gradientDistance - _Params.z);
                }
                
                val = clamp(val, 0, 1);
                color.a *= val;
                return color;
            }
            ENDCG
        }
    }
}
