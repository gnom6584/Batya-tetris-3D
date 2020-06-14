Shader "Hidden/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Thickness("Thickness", float) = 0.0
		_Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _MainTex1;
			float _Thickness;
			fixed4 _Color;

            half4 frag (v2f i) : SV_Target
            {
				half ar = _ScreenParams.x / _ScreenParams.y;
                half4 r = tex2D(_MainTex, i.uv + _Thickness * half2(1.0h, 0.0h));
                half4 l = tex2D(_MainTex, i.uv + _Thickness * half2(-1.0h, 0.0h));
                half4 u = tex2D(_MainTex, i.uv + _Thickness * half2(0.0h, ar));
                half4 d = tex2D(_MainTex, i.uv + _Thickness * half2(0.0h, -ar));
				half4 r1 = tex2D(_MainTex, i.uv + _Thickness * half2(1.0h, ar));
                half4 r2 = tex2D(_MainTex, i.uv + _Thickness * half2(-1.0h, ar));
                half4 r3 = tex2D(_MainTex, i.uv + _Thickness * half2(1.0h, -ar));
                half4 r4 = tex2D(_MainTex, i.uv + _Thickness * half2(-1.0h, -ar));
				half4 t = tex2D(_MainTex, i.uv);
				half4 t1 = tex2D(_MainTex1, i.uv);
				t.a = (r.a + l.a + u.a + d.a + r1.a + r2.a + r3.a + r4.a);
                return lerp(t1, t, saturate(t.a));
            }
            ENDCG
        }
    }
}
