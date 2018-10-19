/// <summary>
/// 「スカイドーム用のシェーダー」
/// 引数　 ：テクスチャ１～２、全体のテクスチャの透過度、テクスチャ１～２のブレンド値の対比、テクスチャ１～２の回転速度
/// 戻り値 ：色情報
/// １．インスペクターでテクスチャ１と２を設定する
/// ２．プロパティで好きな値を調整してSkydomeをアレンジする
/// 作成者：志村まさき
/// </summary>
Shader "Custom/Skydome" {
	Properties{
		//テクスチャ
		_MainTex("Base", 2D) = "white" {}
		_SubTex("Sub Texture", 2D) = "white" {}
		_AlphaValue("Alpha Value", Range(0,1)) = 0.5		//全体の透過設定
		_Blend("Blend", Range(0,1)) = 0.0					//テクスチャブレンドの対比

		//回転速度
		_ScrollX("Scroll X", float) = 0
		_ScrollY("Scroll Y", float) = 0
		_SubScrollX("SubScroll X", float) = 0
		_SubScrollY("SubScroll Y", float) = 0
	}

		SubShader{
		Tags{ "Queue" = "Transparent" }
		Cull Front

		CGPROGRAM

//サーフェスシェーダーのスクリプト宣言
#pragma surface surf Lambert alpha

	struct Input {
		float2 uv_MainTex;
	};

	//変数宣言
	sampler2D _MainTex;
	sampler2D _SubTex;
	float _ScrollX, _ScrollY;
	float _SubScrollX, _SubScrollY;
	half _Blend;
	float _AlphaValue;

	void surf(Input IN, inout SurfaceOutput o) {
		//回転処理
		float2 scroll = float2(_ScrollX, _ScrollY) * _Time.y;
		float2 subScroll = float2(_SubScrollX, _SubScrollY) * _Time.y;

		//スクロール後のピクセルデータ取得
		fixed4 c1 = tex2D(_MainTex, IN.uv_MainTex + scroll);
		fixed4 c2 = tex2D(_SubTex, IN.uv_MainTex + subScroll);
		
		//ブレンド処理
		o.Albedo = lerp(c1.rgb, c2.rgb ,_Blend); 

		//全体の透過設定
		o.Alpha = _AlphaValue;
	}

	ENDCG
	}

	FallBack "Diffuse"
}