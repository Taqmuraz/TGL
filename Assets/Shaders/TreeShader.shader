Shader "Custom/TreeShader" {
	Properties {
		_MainTex_0 ("texture_0 (RGB)", 2D) = "white" {}
		_MainTex_1 ("texture_1 (RGB)", 2D) = "white" {}
		_MainTex_2 ("texture_2 (RGB)", 2D) = "white" {}
		_TexMap ("TextureMap (RGB)", 2D) = "white" {}
		_Tiling_0 ("Tiling_0", float) = 1.0
		_Tiling_1 ("Tiling_1", float) = 1.0
		_Tiling_2 ("Tiling_2", float) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex_0;
		sampler2D _MainTex_1;
		sampler2D _MainTex_2;
		sampler2D _TexMap;

		struct Input {
			float2 uv_MainTex_0;
			float2 uv_MainTex_1;
			float2 uv_MainTex_2;
		};
		float _Tiling_0;
		float _Tiling_1;
		float _Tiling_2;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color

			fixed4 c_0 = tex2D (_MainTex_0, IN.uv_MainTex_0 * _Tiling_0);
			fixed4 c_1 = tex2D (_MainTex_1, IN.uv_MainTex_1 * _Tiling_1);
			fixed4 c_2 = tex2D (_MainTex_2, IN.uv_MainTex_2 * _Tiling_2);

			fixed4 t_color = tex2D (_TexMap, IN.uv_MainTex_0);

			fixed4 cur = ((c_0 * t_color.r) + (c_1 * t_color.g) + (c_2 * t_color.b)) / (t_color.r + t_color.g + t_color.b);

			fixed4 c = cur;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = 0.0;
			o.Smoothness = 0.0;
			o.Alpha = 1.0;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
