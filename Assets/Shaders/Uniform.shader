// Made with Amplify Shader Editor v1.9.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Your Shader/Uniform"
{
	Properties
	{
		_Primary("Primary", Color) = (1,0,0,0)
		_Secondary("Secondary", Color) = (0,0.1333122,1,0)
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

		uniform float4 _Primary;
		uniform float4 _Secondary;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord45 = i.uv_texcoord + float2( 0.25,0 );
			float2 appendResult10_g3 = (float2(0.5 , 0.5));
			float2 temp_output_11_0_g3 = ( abs( (uv_TexCoord45*2.0 + -1.0) ) - appendResult10_g3 );
			float2 break16_g3 = ( 1.0 - ( temp_output_11_0_g3 / fwidth( temp_output_11_0_g3 ) ) );
			float4 lerpResult48 = lerp( _Primary , _Secondary , saturate( min( break16_g3.x , break16_g3.y ) ));
			o.Albedo = lerpResult48.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19200
Node;AmplifyShaderEditor.LerpOp;48;-49.14246,-182.4329;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;202,-185;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Your Shader/Uniform;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.FunctionNode;47;-332.1425,-127.4329;Inherit;True;Rectangle;-1;;3;6b23e0c975270fb4084c354b2c83366a;0;3;1;FLOAT2;0,0;False;2;FLOAT;0.5;False;3;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;50;-358.1425,-319.4329;Inherit;False;Property;_Secondary;Secondary;0;0;Create;True;0;0;0;False;0;False;0,0.1333122,1,0;0,0.1333122,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;49;-347.1425,-505.4329;Inherit;False;Property;_Primary;Primary;0;0;Create;True;0;0;0;False;0;False;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;46;-889.1425,-78.43286;Inherit;False;Constant;_Vector0;Vector 0;0;0;Create;True;0;0;0;False;0;False;0.25,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;45;-636.1425,-130.4329;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
WireConnection;48;0;49;0
WireConnection;48;1;50;0
WireConnection;48;2;47;0
WireConnection;0;0;48;0
WireConnection;47;1;45;0
WireConnection;45;1;46;0
ASEEND*/
//CHKSM=BB6B77B773671DC420401562111E3678211CC950