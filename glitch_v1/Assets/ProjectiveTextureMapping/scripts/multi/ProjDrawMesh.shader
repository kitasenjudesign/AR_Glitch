// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/ProjDrawMesh"
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
			#pragma multi_compile_instancing
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			#include "../noise/SimplexNoise3D.hlsl"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 uv : TEXCOORD0;
				float4 screenPos : TEXCOORD2;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			sampler2D _MainTex;
			
			
			//float4x4 _tMat;
			//float _ScaleY;
			float _Amp;
			float4 _MainTex_ST;
			float _GlobalInvert = 0;

            UNITY_INSTANCING_CBUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color) // Make _Color an instanced property (i.e. an array)
				UNITY_DEFINE_INSTANCED_PROP(float4x4, _ModelMat) // Make _Color an instanced property (i.e. an array)
				UNITY_DEFINE_INSTANCED_PROP(float4x4, _ProjMat) // Make _Color an instanced property (i.e. an array)
				UNITY_DEFINE_INSTANCED_PROP(float4x4, _ViewMat) // Make _Color an instanced property (i.e. an array)
				
            UNITY_INSTANCING_CBUFFER_END

			v2f vert (appdata v)
			{
				v2f o;

			    //インスタンス ID がシェーダー関数にアクセス可能になります。頂点シェーダーの最初に使用する必要があります
                UNITY_SETUP_INSTANCE_ID (v);

                //頂点シェーダーで入力構造体から出力構造体へインスタンス ID をコピーします。
                //フラグメントシェーダーでは、インスタンスごとのデータにアクセスするときのみ必要です
                UNITY_TRANSFER_INSTANCE_ID (v, o);


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

				float4x4 modelMat = UNITY_ACCESS_INSTANCED_PROP(_ModelMat);
				float4x4 viewMat =  UNITY_ACCESS_INSTANCED_PROP(_ViewMat);
				float4x4 projMat =  UNITY_ACCESS_INSTANCED_PROP(_ProjMat);

				float4 worldPos = mul(modelMat,float4(v.vertex.xyz, 1.0));
				float4 viewPos = mul( viewMat, worldPos );
				float4 projPos = mul( projMat, viewPos );
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

                UNITY_SETUP_INSTANCE_ID (i);
                // 変数にアクセス
                //fixed4 col = UNITY_ACCESS_INSTANCED_PROP(_Color);

				// sample the texture
				//fixed4 col = tex2DProj(texture, i.tangent);//tex2D(_MainTex, i.uv);
				
				float2 uv = i.screenPos.xy/i.screenPos.w;
				if(_GlobalInvert == 1){
					uv.y = 1 - uv.y;
				}
				//
				//o.tangent = float4(uv,0,0);

				//fixed4 col =tex2D(_MainTex, i.tangent.xy / i.tangent.w);
				fixed4 col = tex2D( _MainTex, uv);//i.tangent.xy );
				
				//debug用
				//fixed4 col = fixed4(uv.x,uv.y,0,1);
				
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
