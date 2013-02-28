Shader "Custom/AdditiveColoredUntextured" {
	Properties {
		_Color("Main Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Pass {
			 Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    		Lighting Off Cull Off Zwrite On Fog { Mode Off }
    		Blend SrcAlpha One
			LOD 200
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
	
			float4 _Color;
	
			struct v2f {
				float4 pos : SV_POSITION;
			};
			
			v2f vert(appdata_base v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
			
			half4 frag(v2f i) : COLOR {
				return _Color;
			}
			ENDCG
		} 
	}
}
