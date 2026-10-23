// UI 图片外发光描边。
// 原理：当前像素附近如果存在不透明像素，就在透明区域叠加一层发光颜色。
// 注意：图片边缘需要留透明 padding，否则发光会被贴图边界裁掉。
Shader "UI/OuterGlow"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _GlowColor("Glow Color", Color) = (1,0.85,0.2,1)
        _GlowSize("Glow Size", Range(0, 12)) = 4
        _GlowStrength("Glow Strength", Range(0, 5)) = 1.5
        _GlowSoftness("Glow Softness", Range(0.1, 4)) = 1

        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255

        _ColorMask("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Stencil
        {
            Ref[_Stencil]
            Comp[_StencilComp]
            Pass[_StencilOp]
            ReadMask[_StencilReadMask]
            WriteMask[_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest[unity_GUIZTestMode]
        Blend One OneMinusSrcAlpha
        ColorMask[_ColorMask]

        Pass
        {
            Name "Default"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex        : SV_POSITION;
                fixed4 color         : COLOR;
                float2 texcoord      : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float4 mask          : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _GlowColor;
            float _GlowSize;
            float _GlowStrength;
            float _GlowSoftness;
            float4 _MainTex_TexelSize;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _UIMaskSoftnessX;
            float _UIMaskSoftnessY;
            int _UIVertexColorAlwaysGammaSpace;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                float4 vPosition = UnityObjectToClipPos(v.vertex);
                OUT.worldPosition = v.vertex;
                OUT.vertex = vPosition;

                float2 pixelSize = vPosition.w;
                pixelSize /= float2(1, 1) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

                float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
                OUT.texcoord = TRANSFORM_TEX(v.texcoord.xy, _MainTex);
                OUT.mask = float4(v.vertex.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_UIMaskSoftnessX, _UIMaskSoftnessY) + abs(pixelSize.xy)));

                if (_UIVertexColorAlwaysGammaSpace)
                {
                    if (!IsGammaSpace())
                    {
                        v.color.rgb = UIGammaToLinear(v.color.rgb);
                    }
                }

                OUT.color = v.color * _Color;
                return OUT;
            }

            half SampleAlpha(float2 uv)
            {
                return tex2D(_MainTex, uv).a;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                const half alphaPrecision = half(0xff);
                const half invAlphaPrecision = half(1.0 / alphaPrecision);
                IN.color.a = round(IN.color.a * alphaPrecision) * invAlphaPrecision;

                fixed4 spriteColor = IN.color * (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd);

                // 用多圈 alpha 采样估算边缘外侧的发光强度。
                float2 texel = _MainTex_TexelSize.xy * _GlowSize;
                half nearAlpha = 0;
                half farAlpha = 0;

                nearAlpha += SampleAlpha(IN.texcoord + texel * float2( 1,  0));
                nearAlpha += SampleAlpha(IN.texcoord + texel * float2(-1,  0));
                nearAlpha += SampleAlpha(IN.texcoord + texel * float2( 0,  1));
                nearAlpha += SampleAlpha(IN.texcoord + texel * float2( 0, -1));
                nearAlpha += SampleAlpha(IN.texcoord + texel * float2( 0.707,  0.707));
                nearAlpha += SampleAlpha(IN.texcoord + texel * float2(-0.707,  0.707));
                nearAlpha += SampleAlpha(IN.texcoord + texel * float2( 0.707, -0.707));
                nearAlpha += SampleAlpha(IN.texcoord + texel * float2(-0.707, -0.707));
                nearAlpha *= 0.125;

                farAlpha += SampleAlpha(IN.texcoord + texel * _GlowSoftness * float2( 2,  0));
                farAlpha += SampleAlpha(IN.texcoord + texel * _GlowSoftness * float2(-2,  0));
                farAlpha += SampleAlpha(IN.texcoord + texel * _GlowSoftness * float2( 0,  2));
                farAlpha += SampleAlpha(IN.texcoord + texel * _GlowSoftness * float2( 0, -2));
                farAlpha += SampleAlpha(IN.texcoord + texel * _GlowSoftness * float2( 1.414,  1.414));
                farAlpha += SampleAlpha(IN.texcoord + texel * _GlowSoftness * float2(-1.414,  1.414));
                farAlpha += SampleAlpha(IN.texcoord + texel * _GlowSoftness * float2( 1.414, -1.414));
                farAlpha += SampleAlpha(IN.texcoord + texel * _GlowSoftness * float2(-1.414, -1.414));
                farAlpha *= 0.125;

                half sourceAlpha = spriteColor.a;
                half glowAlpha = saturate((nearAlpha + farAlpha * 0.5) * _GlowStrength);
                glowAlpha *= saturate(1 - sourceAlpha);
                glowAlpha *= _GlowColor.a;

                fixed4 glowColor = _GlowColor;
                glowColor.a = glowAlpha;
                glowColor.rgb *= glowColor.a;

                // UI/Default 使用预乘 alpha，这里保持同样输出规则。
                spriteColor.rgb *= spriteColor.a;
                fixed4 color = spriteColor + glowColor * (1 - spriteColor.a);

                #ifdef UNITY_UI_CLIP_RECT
                half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(IN.mask.xy)) * IN.mask.zw);
                color.a *= m.x * m.y;
                color.rgb *= m.x * m.y;
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip(color.a - 0.001);
                #endif

                return color;
            }
            ENDCG
        }
    }
}
