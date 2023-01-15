// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "LP Graphics Pack/Environment/LP Water V0.2B-Specular" {
	//Version 0.2B of the LP Water shader included in the LP Enviro Pack; has dynamic specular reflection enabled
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_SpecColor("Specular Material Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_WaveHeight("Wave Height", float) = 1
		_WaveLength("Wave Length", float) = 1
		_WaveSpeed("Wave Speed", float) = 1
		_WaveHeightDeviaton("Wave Height Random", float) = 1
		_WaveSpeedDeviaton("Wave Speed Random", float) = 1
		_Transparency("Transparency", Range(0,1)) = 0.5
		
	}

	SubShader 
	{
		Pass{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "LightMode"="ForwardBase"}
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		//#pragma surface surf Standard fullforwardshadows alpha:fade
		#include "UnityCG.cginc"
		#pragma vertex vert
		#pragma geometry geom
		#pragma fragment frag
		#pragma target 3.0

		sampler2D _MainTex;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float _WaveHeight;
		float _WaveLength;
		float _WaveSpeed;
		float _WaveHeightDeviaton;
		float _WaveSpeedDeviaton;
		float _Transparency;

		uniform float4 _LightColor0;
		uniform float4 _SpecColor;

		struct Input {
			float2 uv_MainTex;
		};

		//random number generators	
		float rand(float3 co)
		{
			return frac(sin(dot(co.xyz ,float3(12.9898,78.233,45.5432))) * 43758.5453);
		}

		float rand2(float3 co)
		{
			return frac(sin(dot(co.xyz ,float3(19.9128,75.2,34.5122))) * 12765.5213);
		}

		struct vertData
		{
			float4 pos : SV_POSITION;
			float3 norm : NORMAL;
			float2 uv : TEXCOORD0;
		};

		struct geomData
		{
			float4 pos : SV_POSITION;
			float3 norm : NORMAL;
			float2 uv : TEXCOORD0;
			float3 diffuseColor : TEXCOORD1;
			float3 specularColor : TEXCOORD2;
		};

		//manipulates vertices
		vertData vert(appdata_full vInput){
			float4 worldpos = mul(unity_ObjectToWorld, vInput.vertex);
	
			float waveDisplacement = cos((_Time * _WaveSpeed) + (worldpos.y * _WaveLength + rand2(worldpos.yxz)) * (worldpos.x * _WaveLength + 
			rand2(worldpos.xzz)) + (worldpos.z * _WaveLength + rand2(worldpos.zxz))) ;

			worldpos.y += (_WaveHeight) * waveDisplacement;

			vInput.vertex.xyz = mul(unity_WorldToObject, worldpos);

			vertData outPut;
			outPut.pos = vInput.vertex;
			outPut.norm = vInput.normal;
    		outPut.uv = vInput.texcoord;
    		return outPut;

		}

		//manipulates triangles
		[maxvertexcount(3)]
		void geom(triangle vertData IN[3], inout TriangleStream<geomData> Tri){
				float3 v0 = IN[0].pos.xyz;
				float3 v1 = IN[1].pos.xyz;
				float3 v2 = IN[2].pos.xyz;

				float3 centerPos = (v0 + v1 + v2) / 3.0;

				float3 vn = normalize(cross(v1 - v0, v2 - v0));
				
				float4x4 modelMatrix = unity_ObjectToWorld;
				float4x4 modelMatrixInverse = unity_WorldToObject;

				float3 normalDirection = normalize(
					mul(float4(vn, 0.0), modelMatrixInverse).xyz);
				float3 viewDirection = normalize(_WorldSpaceCameraPos
					- mul(modelMatrix, float4(centerPos, 0.0)).xyz);
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float attenuation = 1;

				float3 ambientLighting =
					UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;

				float3 diffuseReflection =
					attenuation * _LightColor0.rgb * _Color.rgb
					* max(0.0, dot(normalDirection, lightDirection));

				float3 specularReflection;
				if (dot(normalDirection, lightDirection) < 0.0)
				{
					specularReflection = float3(0.0, 0.0, 0.0);
				}
				else
				{
					specularReflection = attenuation * _LightColor0.rgb
						* _SpecColor.rgb * pow(max(0.0, dot(
							reflect(-lightDirection, normalDirection),
							viewDirection)), _Glossiness);
				}

				geomData OUT;
				OUT.pos = UnityObjectToClipPos(IN[0].pos);
				OUT.norm = vn;
				OUT.uv = IN[0].uv;
				OUT.diffuseColor = ambientLighting + diffuseReflection;
				OUT.specularColor = specularReflection;
				Tri.Append(OUT);

				OUT.pos = UnityObjectToClipPos(IN[1].pos);
				OUT.norm = vn;
				OUT.uv = IN[1].uv;
				OUT.diffuseColor = ambientLighting + diffuseReflection;
				OUT.specularColor = specularReflection;
				Tri.Append(OUT);

				OUT.pos = UnityObjectToClipPos(IN[2].pos);
				OUT.norm = vn;
				OUT.uv = IN[2].uv;
				OUT.diffuseColor = ambientLighting + diffuseReflection;
				OUT.specularColor = specularReflection;
				Tri.Append(OUT);

		}

		//manipulates color data
		float4 frag(geomData IN): COLOR{
				return float4(IN.specularColor +
				IN.diffuseColor, _Transparency);


		}
		ENDCG
		}
	}
	//FallBack "Diffuse"
}