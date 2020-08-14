Shader "Unlit/OutlinedUnlitShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)


        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.5)) = 0.28

    }
    SubShader
    {
    Tags { "RenderType"="Opaque" }
    LOD 100
        Pass {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f {
                    float4 vertex : SV_POSITION;
                    UNITY_FOG_COORDS(0)
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                fixed4 _Color;

                v2f vert (appdata_t v)
                {
                    v2f o;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                fixed4 frag (v2f i) : COLOR
                {
                    fixed4 col = _Color;
                    UNITY_APPLY_FOG(i.fogCoord, col);
                    UNITY_OPAQUE_ALPHA(col.a);
                    return col;
                }
            ENDCG
        }

        Pass {

            Cull Front

            CGPROGRAM

            #pragma vertex VertexProgram
            #pragma fragment FragmentProgram

            half _OutlineWidth;

            float4 VertexProgram(
                    float4 position : POSITION,
                    float3 normal : NORMAL) : SV_POSITION {

                position.xyz += normal * _OutlineWidth;

                return UnityObjectToClipPos(position);

            }

            half4 _OutlineColor;

            half4 FragmentProgram() : SV_TARGET {
                return _OutlineColor;
            }

            ENDCG

        }

    }
}
