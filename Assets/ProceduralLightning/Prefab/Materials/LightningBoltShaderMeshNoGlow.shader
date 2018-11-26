Shader "Custom/LightningBoltShaderMeshNoGlow"
{
	Properties
	{
		_MainTex ("Color (RGB) Alpha (A)", 2D) = "white" {}
		_TintColor ("Tint Color (RGB)", Color) = (1, 1, 1, 1)
		_InvFade ("Soft Particles Factor", Range(0.01, 100.0)) = 1.0
		_JitterMultiplier ("Jitter Multiplier (Float)", Float) = 0
		_Turbulence ("Turbulence (Float)", Float) = 0.0
		_TurbulenceVelocity ("Turbulence Velocity (Vector)", Vector) = (0, 0, 0, 0)
		_SrcBlendMode("SrcBlendMode (Source Blend Mode)", Int) = 5 // SrcAlpha
		_DstBlendMode("DstBlendMode (Destination Blend Mode)", Int) = 1 // One, change to 10 for alpha blend instead of additive blend
    }

    SubShader
	{
        Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent+10" "LightMode"="Always" "PreviewType"="Plane"}
		UsePass "Custom/LightningBoltShaderMesh/LINEPASS"
    }
 
    Fallback Off
}