Shader "GlowOutline" 
{ 
    Properties 
    { 
        _Color ("Main Color", Color) = (0.0,0.0,0.0,1.0) 
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1) 
        _Outline ("Outline width", Range (0.002, 0.03)) = 0.01
        _MainTex ("Color (RGB) Alpha (A)", 2D) = "white"
        _BumpMap ("Bump (RGB) Illumin (A)", 2D) = "bump" {}
        _RimColor ("Rim Color", Color) = (1,1,1,1)
        _RimPower ("Rim Power", Range(1.0, 6.0)) = 3.0
    } 
 
    SubShader { 
        	Tags {"Queue"="Transparent" "RenderType"="Transparent" "LightMode" = "ForwardBase"} 
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma surface surf Lambert
            
            sampler2D _MainTex;
            sampler2D _BumpMap;
            float4 _Color;
            float4 _RimColor;
            float _RimPower;
            
            struct Input {
                float4 color : Color;
                float2 uv_MainTex;
                float2 uv_BumpMap;
                float3 viewDir;
            };
            
            void surf (Input IN, inout SurfaceOutput o) {
				IN.color = _Color;
                fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha = c.a;
                o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
         
                half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
                o.Emission = _RimColor.rgb * pow(rim, _RimPower);
            }
            
            ENDCG
        Pass 
        { 
            Name "OUTLINE" 
            Tags { "LightMode" = "Always" "RenderType"="Transparent"} 
            
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            // Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct appdata members vertex,normal)
            #pragma exclude_renderers d3d11 xbox360
            #pragma exclude_renderers gles
            #pragma exclude_renderers xbox360
            #pragma vertex vert 
            struct appdata { 
                float4 vertex; 
                float3 normal; 
            }; 
            
            struct v2f { 
                float4 pos : POSITION; 
                float4 color : COLOR; 
                float fog : FOGC; 
            }; 
            uniform float _Outline; 
            uniform float4 _OutlineColor;
            uniform float4 _Color;
            
            v2f vert(appdata v) { 
                v2f o; 
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex); 
                float3 norm = mul ((float3x3)UNITY_MATRIX_MV, v.normal); 
                norm.x *= UNITY_MATRIX_P[0][0]; 
                norm.y *= UNITY_MATRIX_P[1][1]; 
                o.pos.xy += norm.xy * o.pos.z * _Outline; 
                
                o.fog = o.pos.z; 
                o.color = _OutlineColor * _Color.a;
                return o; 
            }
            ENDCG 
            
            Cull Front
            ZWrite Off
            ColorMask RGBA
            Blend SrcAlpha OneMinusSrcAlpha
            SetTexture [_MainTex] { combine primary } 
        } 
    } 
    
    Fallback "Diffuse" 
}
