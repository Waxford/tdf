Shader "Custom/LitPixels" {
	Properties {
		_MainTex ("Texture ", 2D) = "white" {}
		_Normal ("Normal", 2D) = "white" {}
	}
	SubShader {
		Tags { 
			"RenderType"="Transparent"
            "Queue" = "Transparent" 
            }
		LOD 200
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha 
		
		CGPROGRAM
        #pragma surface surf BlinnPhongColor alpha
        #include "UnityCG.cginc"
 
        //custom surface output structure
        struct SurfaceOutputSpecColor {
            half3 Albedo;
            half3 Normal;
            half3 Emission;
            half Alpha;
        };
 
        sampler2D _MainTex;
        sampler2D _Normal;
 
        //forward lighting function
        inline half4 LightingBlinnPhongColor (SurfaceOutputSpecColor s, half3 lightDir, half3 viewDir, half atten) {
          half3 h = normalize (lightDir + viewDir);
 
          half diff = max (0, dot (s.Normal, lightDir));
 
          float nh = max (0, dot (s.Normal, h));
 
          half4 c;
          c.rgb = (s.Albedo * _LightColor0.rgb * diff) * (atten * 2);
          c.a = s.Alpha;//dot(s.Normal.xyz,viewDir) * s.Alpha;
          return c;
        }
 
        struct Input {
            float2 uv_MainTex;
        };
         
        void surf (Input IN, inout SurfaceOutputSpecColor o) {
			half4 col = tex2D(_MainTex, IN.uv_MainTex);
			half4 norm = tex2D(_Normal, IN.uv_MainTex);
            o.Albedo = col.rgb;
            o.Normal = normalize(norm.rgb);
            o.Alpha = col.a;
        }
        ENDCG
	} 
	FallBack "Diffuse"
}
