// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/ProjObj"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Amp ("_Amp",float) = 0.2

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }//"DisableBatching"="True" }
		LOD 100
		Cull off
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			#include "./noise/SimplexNoise3D.hlsl"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 uv : TEXCOORD0;
				float4 screenPos : TEXCOORD2;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			
			
			float4x4 _tMat;
			float4x4 _modelMat;
			float4x4 _projMat;
			float4x4 _viewMat;
			float _ScaleY;
			float _Amp;
			float4 _MainTex_ST;
			
			/*
		_tMat = new Matrix4x4(
			new Vector4(0.5f,0,0,0),//m00,m10,m20,m30
			new Vector4(0,0.5f,0,0),//m01,m11,m21,m31
			new Vector4(0,0,1f,0),//m02,m12,m22,m32
			new Vector4(0.5f,0.5f,0,1f)//m03,m13,m23,m33
		);*/

			/*
            const float4x4 mat = float4x4(
				float4(0.5,0,0,0),
                float4(0,0.5,0,0),
                float4(0,0,1,0),
                float4(0.5,0.5,0,1);
			*/

			v2f vert (appdata v)
			{
				v2f o;
				

				float3 vv = v.vertex.xyz;
				//float yy = 1 + 6 * (0.5 + 0.5 * sin(_ScaleY-3.1415/2));
				//vv.y = sign(vv.y) * 0.5 * pow( abs( vv.y )*2, 1/yy ) * yy;

				float amp = length(vv);
				float radX = (-atan2(vv.z, vv.x) + 3.1415 * 0.5); //+ vv.y * sin(_count) * nejireX;//横方向の角度
				float radY = asin(vv.y / amp);

				float dAmp	= snoise( vv.xyz * 1.6 + _Time.y ) * _Amp;            
				float dRadX	= sin( vv.y + _Time.z) * 0.1;// * _RotAmount;//横方向の角度
				
				amp += 0;//dAmp;// * step(_Th,dAmp);// * _DeformRatio;
				//radX 	+= dRadX;

				//amp = lerp(amp,_Limit,_Sphere);
				
				////to xy coodinate
				vv.x = amp * sin( radX ) * cos( radY );//横
				vv.y = amp * sin( radY );//縦
				vv.z = amp * cos( radX ) * cos( radY );//横

				v.vertex.xyz = vv.xyz;
				o.vertex = UnityObjectToClipPos(v.vertex);




				float4 worldPos = mul(_modelMat,float4(v.vertex.xyz, 1.0));
				float4 viewPos = mul( _viewMat, worldPos );
				float4 projPos = mul( _projMat, viewPos );
				float4 screenPos = ComputeScreenPos(projPos);
				
				//float2 uv = screenPos.xy/screenPos.w;
				//uv.y = 1 - uv.y;
				//o.tangent = float4(uv,0,0);
				o.screenPos = screenPos;
				
				//mul(_tMat, projPos);//TRANSFORM_TEX(v.uv, _MainTex);

				/*
					float4 faceWorldPos = mul(_faceMatrix,IN.vertex);
					float4 faceProjPos = mul( UNITY_MATRIX_VP, faceWorldPos );
					float4 faceScreenPos = ComputeScreenPos(faceProjPos);
					float2 uv = faceScreenPos.xy/faceScreenPos.w;
				*/

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2DProj(texture, i.tangent);//tex2D(_MainTex, i.uv);
				
				float2 uv = i.screenPos.xy/i.screenPos.w;
				//uv.y = 1 - uv.y;
				//uv.y = 1 - uv.y;
				//o.tangent = float4(uv,0,0);

				//fixed4 col =tex2D(_MainTex, i.tangent.xy / i.tangent.w);
				fixed4 col = tex2D( _MainTex, uv);//i.tangent.xy );
				
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
