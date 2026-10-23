Shader "Custom/SpriteFogMask"
{
    Properties
    {
        _MainTex ("Sprite", 2D) = "white" {}
        _Color   ("Tint", Color) = (0,0,0,1)   // 建议颜色为黑，alpha=1
        _MaskTex ("Mask (R channel)", 2D) = "white" {}
        _WorldMin ("World Min (xy)", Vector) = (0,0,0,0)
        _WorldSize("World Size (xy)", Vector) = (1,1,0,0)
    }
    SubShader
    {
        Tags{ "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline" = "UniversalPipeline"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4    _MainTex_ST;
            sampler2D _MaskTex;
            float4    _Color;
            float4    _WorldMin;   // xy 有效
            float4    _WorldSize;  // xy 有效

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0; // 供主纹理
                fixed4 color  : COLOR;
            };

            struct v2f
            {
                float4 pos  : SV_POSITION;
                float2 uv   : TEXCOORD0;
                float4 wpos : TEXCOORD1; // 世界坐标
                fixed4 col  : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos  = UnityObjectToClipPos(v.vertex);
                o.uv   = TRANSFORM_TEX(v.uv, _MainTex);
                o.wpos = mul(unity_ObjectToWorld, v.vertex);
                o.col  = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 主贴图（其实我们只需要颜色/alpha，通常是一张纯黑或纯透明的图）
                fixed4 col = tex2D(_MainTex, i.uv) * i.col;

                // 世界坐标 -> 遮罩 UV [0,1]
                float2 uvMask = (i.wpos.xy - _WorldMin.xy) / _WorldSize.xy;

                // 超出范围的默认遮罩=1（全黑）。在 0..1 采样，否则 1
                float mask = 1.0;
                if (all(uvMask >= 0) && all(uvMask <= 1))
                {
                    // 约定：遮罩采样 R 通道：0 = 通透，1 = 遮挡
                    mask = tex2D(_MaskTex, uvMask).a;
                    // mask = 0;
                }

                // 只乘 alpha：颜色用黑色（_Color.rgb）
                col.a *= saturate(mask);
                return col;
            }
            ENDCG
        }
    }
}
