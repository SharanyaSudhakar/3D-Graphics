Shader "ShaderSamples/NormalTexShader"
{
    Properties
    {
        _MainTint ("Diffuse Tint", Color) = (0,1,0,1)
        _NormalTex ("Normal Map", 2D) = "bump" {}
		_NormalIntensity("Normal Intensity Value", Range(0,3)) = 1
		_ScrollSpeed("Scroll Speed", Range(0,10)) = 2
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _NormalTex;

        struct Input
        {
			//the models uvs
            float2 uv_NormalTex;
        };

		//float has higher precision than fixed.
        float4 _MainTint;
		float _NormalIntensity;
		float _ScrollSpeed;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			o.Albedo = _MainTint;
			//tint material is base color;

			fixed2 scrollUV = IN.uv_NormalTex;
			fixed scrollOffset = _ScrollSpeed * _Time;
			scrollUV += fixed2(scrollOffset, 0);
			//get normal info by unpacking
			float3 normalmap = UnpackNormal(tex2D(_NormalTex, scrollUV));

			normalmap.x *= _NormalIntensity;
			normalmap.y *= _NormalIntensity;
			//aply new normal
			o.Normal = normalize(normalmap.rgb);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
