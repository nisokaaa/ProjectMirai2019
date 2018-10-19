Shader "Custom/Skydome" {
	Properties{
		_MainTex("Base", 2D) = "white" {}
		_SubTex("Sub Texture", 2D) = "white" {}
		_Sub2Tex("Sub2 Texture", 2D) = "white" {}

		//回転速度
		_ScrollX("Scroll X", float) = 0
		_ScrollY("Scroll Y", float) = 0
		_SubScrollX("SubScroll X", float) = 0
		_SubScrollY("SubScroll Y", float) = 0
		_Sub2ScrollX("Sub2Scroll X", float) = 0
		_Sub2ScrollY("Sub2Scroll Y", float) = 0

		//合成値
		_Blend("Blend", Range(0,1)) = 0.0 // 追加
	}

		SubShader{
		Tags{ "Queue" = "Transparent" }
		Cull Front

		CGPROGRAM

#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _SubTex;
		sampler2D _Sub2Tex;

	struct Input {
		float2 uv_MainTex;
	};

	float _ScrollX, _ScrollY;
	float _SubScrollX, _SubScrollY;
	float _Sub2ScrollX, _Sub2ScrollY;
	half _Blend; // 追加

	void surf(Input IN, inout SurfaceOutput o) {
		//移動量
		float2 scroll = float2(_ScrollX, _ScrollY) * _Time.y;
		float2 subScroll = float2(_SubScrollX, _SubScrollY) * _Time.y;
		float2 sub2Scroll = float2(_Sub2ScrollX, _Sub2ScrollY) * _Time.y;
		//o.Albedo = tex2D(_MainTex, IN.uv_MainTex + scroll);
		

		//テクスチャのブレンド
		fixed4 c1 = tex2D(_MainTex, IN.uv_MainTex + scroll);
		fixed4 c2 = tex2D(_SubTex, IN.uv_MainTex + subScroll);
		fixed4 c3 = tex2D(_Sub2Tex, IN.uv_MainTex + sub2Scroll);
		//fixed4 p = tex2D(_MaskTex, IN.uv_MainTex);
		//fixed4 test = lerp(c1, c2);
		o.Albedo = lerp(c1.rgb, c2.rgb ,_Blend); // 修正

		//c1 = lerp(c1.rgb, c2.rgb, _Blend); // 修正
		//o.Albedo = lerp(testCollar, c3.rgb, _Blend); // 修正
		o.Alpha = c1.a;

		//UVスクロール
		//half4 color = tex2D(_MainTex, IN.uv_MainTex + scroll);		//一枚テクスチャ×スクロール

		//ブレンド
		//o.Albedo = lerp(c1, c2, p);
		//half4 color = tex2D(lerp(c1, c2, p), IN.uv_MainTex + scroll);

		//o.Albedo = test.rgb;	//色情報を入れる
		//o.Alpha = test.a;		//透過情報を入れる
		
		//o.Albedo = color.rgb;	//色情報を入れる
		//o.Alpha = color.a;		//透過情報を入れる
		
	}

	ENDCG
	}

	FallBack "Diffuse"
}