Shader "Custom/Skydome" {
	Properties{
		_MainTex("Base", 2D) = "white" {}
		_SubTex("Sub Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
		_ScrollX("Scroll X", float) = 0
		_ScrollY("Scroll Y", float) = 0
	}

		SubShader{
		Tags{ "Queue" = "Transparent" }
		Cull Front

		CGPROGRAM

#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _SubTex;
		sampler2D _MaskTex;

	struct Input {
		float2 uv_MainTex;
		float2 uv_SubTex;
	};

	float _ScrollX, _ScrollY;

	void surf(Input IN, inout SurfaceOutput o) {
		//移動量
		float2 scroll = float2(_ScrollX, _ScrollY) * _Time.y;
		o.Albedo = tex2D(_MainTex, IN.uv_MainTex + scroll);

		//テクスチャのブレンド
		fixed4 c1 = tex2D(_MainTex, IN.uv_MainTex);
		fixed4 c2 = tex2D(_SubTex, IN.uv_SubTex);
		fixed4 p = tex2D(_MaskTex, IN.uv_MainTex);
		o.Albedo = lerp(c1, c2, p);

		//UVスクロール
		half4 color = tex2D(_MainTex, IN.uv_MainTex + scroll);
		
		//ブレンド


		//half4 color = tex2D(lerp(c1, c2, p), IN.uv_MainTex + scroll);
		o.Albedo = color.rgb;
		o.Alpha = color.a;
	}

	ENDCG
	}

	FallBack "Diffuse"
}