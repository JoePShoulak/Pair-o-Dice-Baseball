// Made with Amplify Shader Editor v1.9.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Your Shader/Brick"
{
	Properties
	{
		_Texture("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,0.6431373,0.2784314,1)
		_Blend("Blend", Range( 0 , 1)) = 0.7
		_Scale("Scale", Range( 0 , 100)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Texture;
		uniform float _Scale;
		uniform float4 _Color;
		uniform float _Blend;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float temp_output_8_0 = ( _Scale / 100.0 );
			float2 appendResult11 = (float2(temp_output_8_0 , temp_output_8_0));
			float2 uv_TexCoord12 = i.uv_texcoord * appendResult11;
			float4 blendOpSrc13 = tex2D( _Texture, uv_TexCoord12 );
			float4 blendOpDest13 = _Color;
			float4 lerpBlendMode13 = lerp(blendOpDest13,(( blendOpSrc13 > 0.5 ) ? max( blendOpDest13, 2.0 * ( blendOpSrc13 - 0.5 ) ) : min( blendOpDest13, 2.0 * blendOpSrc13 ) ),_Blend);
			o.Albedo = ( saturate( lerpBlendMode13 )).rgb;
			o.Metallic = 0.0;
			o.Smoothness = 0.5;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19200
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;128,-256;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Your Shader/Brick;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.DynamicAppendNode;11;-960,-384;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-768,-384;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-512,-416;Inherit;True;Property;_Texture;Texture;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;-512,-48;Inherit;False;Property;_Blend;Blend;2;0;Create;True;0;0;0;False;0;False;0.7;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-128,-48;Inherit;False;Constant;_Smoothness;Smoothness;0;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-128,-128;Inherit;False;Constant;_Metallic;Metallic;0;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;13;-160,-256;Inherit;False;PinLight;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;8;-1120,-384;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;100;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1440,-384;Inherit;False;Property;_Scale;Scale;3;0;Create;True;0;0;0;False;0;False;1;1;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;-512,-224;Inherit;False;Property;_Color;Color;1;0;Create;True;0;0;0;False;0;False;1,0.6431373,0.2784314,1;1,0.6431373,0.2784314,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
WireConnection;0;0;13;0
WireConnection;0;3;2;0
WireConnection;0;4;3;0
WireConnection;11;0;8;0
WireConnection;11;1;8;0
WireConnection;12;0;11;0
WireConnection;7;1;12;0
WireConnection;13;0;7;0
WireConnection;13;1;4;0
WireConnection;13;2;5;0
WireConnection;8;0;9;0
ASEEND*/
//CHKSM=D9809B62D5FEE036303099A87870BD2066D0E1B3