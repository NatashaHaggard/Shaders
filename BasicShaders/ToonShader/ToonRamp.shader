Shader "Custom/ToonRamp"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _RampTex("Ramp Texture", 2D) = "white"{}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
	    _Outline ("Outline Width", Range (.002, 0.1)) = .005
    }

    SubShader
    {
        ZWrite off
        CGPROGRAM
	      #pragma surface surf Lambert vertex:vert

	      struct Input {
	          float2 uv_MainTex;
	      };

	      float _Outline;
	      float4 _OutlineColor;

	      void vert (inout appdata_full v) {
	          v.vertex.xyz += v.normal * _Outline; // extruding out the mesh
	      }

	      void surf (Input IN, inout SurfaceOutput o) 
	      {
	          o.Emission = _OutlineColor.rgb;
	      }

        ENDCG

        ZWrite on

        CGPROGRAM
        #pragma surface surf ToonRamp

        float4 _Color;
        sampler2D _RampTex;

        float4 LightingToonRamp (SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            float diff = dot (s.Normal, lightDir);
            float h = diff * 0.5 + 0.5;
            float2 rh = h;
            float3 ramp = tex2D(_RampTex,rh).rgb;

            float4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * (ramp);
            c.a = s.Alpha;
            return c;
        }

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = _Color.rgb;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
