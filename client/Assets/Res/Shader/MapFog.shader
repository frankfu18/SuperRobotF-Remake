Shader "Custom/ArrayTextureDecoder"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _DataTexture ("Data Texture", 2D) = "white" {}
        [IntRange] _Columns ("Columns", Range(1, 32)) = 32
        [IntRange] _Rows ("Rows", Range(1, 32)) = 32
//        _TransparentRegion ("Transparent Region (0-1024)", Int) = -1
    }

    SubShader
    {
        Tags 
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_DataTexture);
            SAMPLER(sampler_DataTexture);

            // sampler2D _MainTex;
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                int _TransparentRegion;
                int _Columns;
                int _Rows;
            CBUFFER_END

            uniform float _Blocks[1024]; 
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                

                // // 如果没有设置透明区域，直接返回颜色
                // if (_TransparentRegion < 0 || _TransparentRegion >= (_Columns * _Rows))
                //     return color;

                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);

                float4 data = SAMPLE_TEXTURE2D(_DataTexture, sampler_DataTexture, IN.uv);

                return half4(color.r, color.g, color.b, color.a * data.a);
                
                // // 计算当前像素所在的区域
                // float2 gridUV = IN.uv * float2(_Columns, _Rows);
                // int regionX = floor(gridUV.x);
                // int regionY = floor(gridUV.y);
                //
                // // 确保在有效范围内
                // regionX = clamp(regionX, 0, _Columns - 1);
                // regionY = clamp(regionY, 0, _Rows - 1);
                //
                // // 计算当前区域索引
                // int currentRegion = regionY * _Columns + regionX;
                //
                // float hole = _Blocks[currentRegion];
                // // float fractionalPart = frac(hole);
                // return half4(color.r, color.g, color.b, hole);

                
                // // 如果是目标透明区域，设置alpha为0
                // if (currentRegion == _TransparentRegion)
                //     color.a = 0;

                
                // 采样纹理（假设 uv.x 是线性索引）
                // float4 data = SAMPLE_TEXTURE2D(_DataTexture, sampler_DataTexture, float2(IN.uv.x, 0));
                //
                // // 解码数据（R/G/B/A 分别对应 4 个 float 值）
                // float valueR = data.r; // 第 1 个 float
                // float valueG = data.g; // 第 2 个 float
                // float valueB = data.b; // 第 3 个 float
                // float valueA = data.a; // 第 4 个 float

                
                // int integerPart1 = floor(valueR);
                // int integerPart2 = floor(valueG);
                // int integerPart3 = floor(valueB);
                // int integerPart4 = floor(valueA);

                // float fractionalPart1 = frac(valueR);
                // float fractionalPart2 = frac(valueG);
                // float fractionalPart3 = frac(valueB);
                // float fractionalPart4 = frac(valueA);

                

                // float4 hole = _Blocks[integerPart1];
                // float fractionalPart = frac(hole);
                // return half4(color.r, color.g, color.b, color.a * valueA);
                
                // if(currentRegion == integerPart1)
                // {
                //     float fractionalPart = frac(valueR);
                //     return half4(color.r, color.g, color.b, fractionalPart);
                // }
                
                // for(int j = 0; j < 1024; j++)
                // {
                //     float4 hole = _Blocks[j];
                //     int integerPart = floor(hole);
                //     float fractionalPart = frac(hole);
                //     if(currentRegion == integerPart)
                //     {
                //         return half4(color.r, color.g, color.b, fractionalPart);
                //     }
                // }
                
                // 示例：返回 R 通道的值（仅用于测试）
                // return half4(valueR, valueG, valueB, 1.0);
                // return color;
            }

            void DecodePixel(float val)
            {
                int integerPart = floor(val);          
                float fractionalPart = frac(val);
                
            }
            ENDHLSL
        }
    }
}