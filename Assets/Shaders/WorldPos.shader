// Upgrade NOTE: upgraded instancing buffer 'WorldPos' to new syntax.

// Made with Amplify Shader Editor v1.9.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "WorldPos"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_multi("multi", Range( 0 , 1)) = 0
		_z("z", Range( 0 , 1)) = 0
		_NoiseScale("Noise Scale", Range( 0 , 100)) = 5
		_Float0("Float 0", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float _Float0;
		uniform float _Metallic;
		uniform float _Smoothness;

		UNITY_INSTANCING_BUFFER_START(WorldPos)
			UNITY_DEFINE_INSTANCED_PROP(float, _z)
#define _z_arr WorldPos
			UNITY_DEFINE_INSTANCED_PROP(float, _multi)
#define _multi_arr WorldPos
			UNITY_DEFINE_INSTANCED_PROP(float, _NoiseScale)
#define _NoiseScale_arr WorldPos
		UNITY_INSTANCING_BUFFER_END(WorldPos)


		inline float noise_randomValue (float2 uv) { return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453); }

		inline float noise_interpolate (float a, float b, float t) { return (1.0-t)*a + (t*b); }

		inline float valueNoise (float2 uv)
		{
			float2 i = floor(uv);
			float2 f = frac( uv );
			f = f* f * (3.0 - 2.0 * f);
			uv = abs( frac(uv) - 0.5);
			float2 c0 = i + float2( 0.0, 0.0 );
			float2 c1 = i + float2( 1.0, 0.0 );
			float2 c2 = i + float2( 0.0, 1.0 );
			float2 c3 = i + float2( 1.0, 1.0 );
			float r0 = noise_randomValue( c0 );
			float r1 = noise_randomValue( c1 );
			float r2 = noise_randomValue( c2 );
			float r3 = noise_randomValue( c3 );
			float bottomOfGrid = noise_interpolate( r0, r1, f.x );
			float topOfGrid = noise_interpolate( r2, r3, f.x );
			float t = noise_interpolate( bottomOfGrid, topOfGrid, f.y );
			return t;
		}


		float SimpleNoise(float2 UV)
		{
			float t = 0.0;
			float freq = pow( 2.0, float( 0 ) );
			float amp = pow( 0.5, float( 3 - 0 ) );
			t += valueNoise( UV/freq )*amp;
			freq = pow(2.0, float(1));
			amp = pow(0.5, float(3-1));
			t += valueNoise( UV/freq )*amp;
			freq = pow(2.0, float(2));
			amp = pow(0.5, float(3-2));
			t += valueNoise( UV/freq )*amp;
			return t;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float _z_Instance = UNITY_ACCESS_INSTANCED_PROP(_z_arr, _z);
			float3 ase_worldPos = i.worldPos;
			float _multi_Instance = UNITY_ACCESS_INSTANCED_PROP(_multi_arr, _multi);
			float clampResult31 = clamp( ( ase_worldPos.z * _multi_Instance ) , 0.0 , 0.85 );
			float2 appendResult10 = (float2(_z_Instance , clampResult31));
			float2 uv_TexCoord3 = i.uv_texcoord + appendResult10;
			float2 appendResult24 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 uv_TexCoord19 = i.uv_texcoord * float2( 5,5 ) + appendResult24;
			float _NoiseScale_Instance = UNITY_ACCESS_INSTANCED_PROP(_NoiseScale_arr, _NoiseScale);
			float simpleNoise16 = SimpleNoise( uv_TexCoord19*_NoiseScale_Instance );
			o.Albedo = ( tex2D( _TextureSample0, uv_TexCoord3 ) * ( simpleNoise16 + _Float0 ) ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19200
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;WorldPos;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-381.6766,158.6446;Inherit;False;Property;_Smoothness;Smoothness;2;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-380.6766,66.64462;Inherit;False;Property;_Metallic;Metallic;1;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;10;-1312.677,63.64462;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-839.1228,-12.33349;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;16;-883.5089,357.5592;Inherit;False;Simple;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;24;-1476.234,432.0869;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1245.258,499.5879;Inherit;False;InstancedProperty;_NoiseScale;Noise Scale;5;0;Create;True;0;0;0;False;0;False;5;0;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-1221.01,280.564;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;5,5;False;1;FLOAT2;5,5;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-402.5063,-78.78689;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-559.4962,339.8527;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-877.2137,479.399;Inherit;False;Property;_Float0;Float 0;6;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-2153.706,367.158;Inherit;False;InstancedProperty;_multi;multi;3;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1663.161,16.26248;Inherit;False;InstancedProperty;_z;z;4;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;6;-2347.756,163.0739;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1893.712,206.2227;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1089.326,15.13695;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;31;-1634.751,121.8112;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.85;False;1;FLOAT;0
WireConnection;0;0;28;0
WireConnection;0;3;4;0
WireConnection;0;4;5;0
WireConnection;10;0;11;0
WireConnection;10;1;31;0
WireConnection;1;1;3;0
WireConnection;16;0;19;0
WireConnection;16;1;21;0
WireConnection;24;0;6;1
WireConnection;24;1;6;3
WireConnection;19;1;24;0
WireConnection;28;0;1;0
WireConnection;28;1;30;0
WireConnection;30;0;16;0
WireConnection;30;1;26;0
WireConnection;7;0;6;3
WireConnection;7;1;8;0
WireConnection;3;1;10;0
WireConnection;31;0;7;0
ASEEND*/
//CHKSM=32F50618E710B6EFB7B94D66AAE2892DF25A71C9