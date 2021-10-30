Shader "Custom/Caustics"
{
    Properties
    {
        _CausticsTex("Caustics Texture", 2D) = "white" {}
        _Caustics1_ST("Caustics 1 ST", Vector) = (1,1,0,0)
        _Caustics2_ST("Caustics 2 ST", Vector) = (1,1,0,0)
        _Caustics1Speed("Caustics 1 Speed", Range(0, 1)) = 0.3
        _Caustics2Speed("Caustics 2 Speed", Range(0, 1)) = 0.3
        _SplitRGB("Split Colors", Range(0, 0.1)) = 0.05
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        sampler2D _CausticsTex;
        float4 _Caustics1_ST;
        float4 _Caustics2_ST;
        half _Caustics1Speed;
        half _Caustics2Speed;
        half _SplitRGB;
        

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            fixed4 c2 = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb + c2.rgb;

            fixed2 uv = IN.uv_MainTex * _Caustics1_ST.xy + _Caustics1_ST.zw;
            fixed2 uv2 = IN.uv_MainTex * _Caustics2_ST.xy + _Caustics2_ST.zw;
            uv += _Caustics1Speed * _Time.y;
            uv2 += _Caustics2Speed * _Time.y;

            fixed s = _SplitRGB;
            fixed r = tex2D(_CausticsTex, uv + fixed2(+s * 2, +s)).r;
            fixed g = tex2D(_CausticsTex, uv + fixed2(+s, -s)).g;
            fixed b = tex2D(_CausticsTex, uv + fixed2(-s, -s)).b;
            fixed3 caustics1 = fixed3(r, g, b);

            fixed s2 = _SplitRGB;
            fixed r2 = tex2D(_CausticsTex, uv2 + fixed2(+s2, +s2)).r;
            fixed g2 = tex2D(_CausticsTex, uv2 + fixed2(+s2, -s2)).g;
            fixed b2 = tex2D(_CausticsTex, uv2 + fixed2(-s2, -s2)).b;
            fixed3 caustics2 = fixed3(r2, g2, b2);

            o.Albedo.rgb += min(caustics1, caustics2);

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
