Shader "Universal Render Pipeline/Custom/EmmisiveByPass"
{
    Properties
    {
        _BaseMap("Base Map", 2D) = "white" {}
        _Emission("Emission Color", Color) = (1, 1, 1, 1)
    }
        SubShader
        {
            Tags
            {
                "Queue" = "Transparent+2000" // Use a custom queue value higher than Transparent
            }

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                 ZWrite Off // Disable depth writes
                 ZTest Always // Always pass the depth test

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _BaseMap;
                float4 _Emission;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    half4 baseColor = tex2D(_BaseMap, i.uv);
                    half4 emissive = _Emission;
                    return baseColor + emissive;
                }
                ENDCG
            }
        }
}
