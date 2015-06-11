Shader "Custom/DitherGradient" {
	Properties {
		_DitherMap ("Dither Map (RGB)", 2D) = "white" {}
		_StartLightColor ("Start Light Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_EndLightColor ("End Light Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_StartDimColor ("Start Dim Color ", Color) = (1.0, 1.0, 1.0, 1.0)
		_EndDimColor ("End Dim Color ", Color) = (1.0, 1.0, 1.0, 1.0)
		_DitherWidth ("Dither Width", float) = 5
		_DitherHeight ("Dither Height", float) = 16
		_PixelScale ("Pixel Scale", float) = 32
	}
	SubShader {
		Tags { 
			"RenderType"="Opaque"
            }
		CGPROGRAM
        #pragma surface surf BlinnPhongColor alpha
		struct Input {
		    float2 uv_DitherMap;
		    float4 screenPos;
		    float3 worldPos;
		};
 
        //custom surface output structure
        struct SurfaceOutputCustom {
            half3 Albedo;
            half3 AltAlbedo;
            half3 Normal;
            half3 Emission;
            half Alpha;
        };
 
        //forward lighting function
        inline half4 LightingBlinnPhongColor (SurfaceOutputCustom s, half3 lightDir, half3 viewDir, half atten) {
          half3 h = normalize (lightDir + viewDir);
 
          half diff = max (0, dot (s.Normal, lightDir));
 
 		  float intensity = (_LightColor0.r + _LightColor0.g + _LightColor0.b)/3;
 
          half4 c;
          c.rgb = s.Albedo * (_LightColor0.rgb * diff * atten * 2) + s.AltAlbedo * (1 - _LightColor0.rgb * diff * atten * 2);
          c.a = s.Alpha;//dot(s.Normal.xyz,viewDir) * s.Alpha;
          return c;
        }
		
		sampler2D _DitherMap;
		float4 _StartLightColor;
		float4 _EndLightColor;
		float4 _StartDimColor;
		float4 _EndDimColor;
		float _DitherWidth;
		float _DitherHeight;
		float _PixelScale;

//		float4 _ScreenParams : 
// 		x is current render target width in pixels 
// 		y is current render target height in pixels 
// 		z is 1.0 + 1.0/width 
// 		w is 1.0 + 1.0/height		
 		
		void surf (Input IN, inout SurfaceOutputCustom o) {
			float2 screenToPixelRatio = float2(_ScreenParams.x / _PixelScale, _ScreenParams.y / _PixelScale);
//		    float2 screenUv = float2(IN.screenPos.x * screenToPixelRatio.x, 
//		    						 IN.screenPos.y * screenToPixelRatio.y);
		    float2 screenUv = float2(IN.worldPos.x, IN.worldPos.y);
		    						 
		    float correctedDithermapX = IN.uv_DitherMap.x;// - fmod(IN.uv_DitherMap.x,1f/(_DitherWidth * screenToPixelRatio.x));
		    						 
		    float ditherColumnNumber = (screenUv.x - fmod(screenUv.x,1f/_DitherHeight));
		    //calculate y offsets based off number of columns moved across
		    float ditherColumnOffset = fmod((correctedDithermapX - fmod(correctedDithermapX,1f/_DitherWidth))*_DitherWidth,2f) + 1f;
		    
		    screenUv = float2(correctedDithermapX, (screenUv.y + ditherColumnOffset * ditherColumnNumber));
		    float smpl = tex2D(_DitherMap, screenUv).rgb;
		    float4 lightColor = step(0.5f,smpl.x) * _StartLightColor + step(smpl.x,0.5f) * _EndLightColor;
		    float4 dimColor = step(0.5f,smpl.x) * _StartDimColor + step(smpl.x,0.5f) * _EndDimColor;
		    o.Albedo = lightColor.rgb;
		    o.AltAlbedo = dimColor.rgb;
		    o.Alpha = lightColor.a;
		    o.Alpha = dimColor.a;
		}
		ENDCG
	} 
}
