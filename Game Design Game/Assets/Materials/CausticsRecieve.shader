// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/CausticsRecieve"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _EmissionColor("Emission Color", Color) = (0,0,0)
        _EmissionMap("Emission", 2D) = "white" {}
        _CausticsStartLevel("Caustics Start Level", Float) = 0.0
        _CausticsShallowFadeDistance("Caustics Shallow Distance", Float) = 1.0
        _CausticsScale("Caustics Scale", Float) = 1.0
        _CausticsDrift("Caustics Drift", Vector) = (0.1, 0.0, -0.4)
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
        #pragma shader_feature _EMISSION

        #include "UnityCG.cginc"

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
          float3 worldNormal;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half4 		_EmissionColor;
        sampler2D	_EmissionMap;

        float		_CausticsStartLevel;
        float		_CausticsShallowFadeDistance;
        float		_CausticsScale;
        float3		_CausticsDrift;

        float4x4 _CausticsLightOrientation = float4x4(
                float4(-0.48507, -0.56592, 0.66667, 0.00000),
                float4( 0.72761,  0.16169, 0.66667, 0.00000),
                float4(-0.48507,  0.80845, 0.33333, 0.00000),
                float4( 0.00000,  0.00000, 0.00000, 1.00000)
                );

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

/*         half3 Emission(float2 uv) */
/*         { */
/* #ifndef _EMISSION */
/*           return 0; */
/* #else */
/*           return tex2D(_EmissionMap, uv).rgb * _EmissionColor.rgb; */
/* #endif */
/*         } */

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
          // Albedo comes from a texture tinted by color
          fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
          if (IN.worldPos.y < _CausticsStartLevel) {
            // taken from:
            // https://www.dualheights.se/caustics/caustics-water-texturing-using-unity3d.shtml
            // Move the caustics in world space
            float3 drift = _CausticsDrift * _Time.y;
            // Fade out caustics for shallow water
            float fadeFactor = min(1.0f,
                                   (_CausticsStartLevel - IN.worldPos.y) /
                                   _CausticsShallowFadeDistance);
            // Remove caustics on half bottom of objects, i.e. no caustics "from below"
            float3 upVec = float3(0, 1, 0);
            float belowFactor = min(1.0f, max(0.0f, dot(IN.worldNormal, upVec) + 0.5f));
            // Calculate the projected texture coordinate in the caustics texture
            float3 worldCoord = (IN.worldPos + drift) / _CausticsScale;
            float2 causticsTextureCoord = mul(worldCoord, _CausticsLightOrientation).xy;
            // Calculate caustics light emission
            o.Emission += tex2D(_EmissionMap, causticsTextureCoord) * fadeFactor * belowFactor * _EmissionColor;
          }
          o.Albedo = c.rgb;
          // Metallic and smoothness come from slider variables
          o.Metallic = _Metallic;
          o.Smoothness = _Glossiness;
          o.Alpha = c.a;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
