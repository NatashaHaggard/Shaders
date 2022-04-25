 Shader "Custom/Hologram"{ 

Properties { 
    _RimColor ("Rim Color", Color) = (0,0.5,0.5,0.0)
    _RimIntensity ("Rim Intensity ", Range(0.5,8.0)) = 3.0
}

SubShader {

    Tags{
             "Queue" = "Transparent"
        }

    // Second pass, to hide the inside geometry for a cleaner render
    Pass {
        Zwrite On
        ColorMask 0
    }

    CGPROGRAM
        #pragma surface surf Lambert alpha:fade

        struct Input { 
            float3 viewDir;
        };

        float4 _RimColor;
        float _RimIntensity ;

        void surf (Input IN, inout SurfaceOutput o){ 
            half rim = 1 - saturate(dot(normalize(IN.viewDir), o.Normal));
            o.Emission = _RimColor.rgb * pow(rim,_RimIntensity );
            o.Alpha = pow (rim, _RimIntensity);
        }

    ENDCG

}

FallBack "Diffuse" 

}
