﻿Shader "ShaderSamples/TextureShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_ScrollXdx("X Scroll Speed", Range(0,10)) = 2
		_ScrollYdy("Y Scroll Speed", Range(0,10)) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

		fixed _ScrollXdx;
		fixed _ScrollYdy;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //uv to be scrolled with new values
			fixed2 scrolledUV = IN.uv_MainTex;
			//setting up variables for the new offset values multiplied by by time
			//without time you are only changing the offset values.
			fixed xScroll = _ScrollXdx * _Time;
			fixed yScroll = _ScrollYdy * _Time;
			//_time is unity's game time clock
			//add new values to the uvs.
			scrolledUV += fixed2(xScroll, yScroll);
			// Albedo comes from a texture tinted by color
			//use new uv in texture.
            half4 c = tex2D (_MainTex, scrolledUV);
            o.Albedo = c.rgb * _Color;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}