Shader "Imageblock"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}  // ���������ڴ����ͼ��
        _Amplitude("Amplitude", Range(-1, 0)) = -0.15  // ������λ���������Χ��-1��0֮��
        _Amount("Amount", Range(-5, 5)) = 0.5  // �����λ��ǿ�ȣ���Χ��-5��5֮��
        _BlockSize("Block Size", Range(0, 1)) = 0.05  // ������Ĵ�С����Χ��0��1֮��
        _Speed("Speed", Range(0, 100)) = 10  // ������λ���ٶȣ���Χ��0��100֮��
        _BlockPow("Block Size Pow", Vector) = (3, 3, 0, 0)  // ��������ݴΣ����ڿ������������״
    }
        SubShader
        {
            // �������޳�����Ȳ���
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

                // ������ɫ����������λ�ú�UV���ݵ�Ƭ����ɫ��
                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                sampler2D _MainTex;
                float4 _MainTex_TexelSize;
                float _Amplitude;
                float _Amount;
                float _BlockSize;
                float _Speed;
                float4 _BlockPow;

                // �����������������һ����ά������Ϊ����
                inline float randomNoise(float2 seed)
                {
                    return frac(sin(dot(seed * floor(_Time.y * _Speed), float2(17.13, 3.71))) * 43758.5453123);
                }

                // ����������λ��ǿ��
                float Noise()
                {
                    float _TimeX = _Time.y;
                    float splitAmount = (1.0 + sin(_TimeX * 6.0)) * 0.5;
                    splitAmount *= 1.0 + sin(_TimeX * 16.0) * 0.5;
                    splitAmount *= 1.0 + sin(_TimeX * 19.0) * 0.5;
                    splitAmount *= 1.0 + sin(_TimeX * 27.0) * 0.5;
                    splitAmount = pow(splitAmount, _Amplitude);
                    splitAmount *= (0.05 * _Amount);
                    return splitAmount;
                }

                // ����ÿ�����ص�������λ��ǿ��
                float ImageBlockIntensity(v2f i)
                {
                    float2 size = lerp(1, _MainTex_TexelSize.xy, 1 - _BlockSize);
                    size = floor((i.uv) / size);
                    float noiseBlock = randomNoise(size);
                    float displaceNoise = pow(noiseBlock, _BlockPow.x) * pow(noiseBlock, _BlockPow.y);
                    return displaceNoise;
                }

                // Ƭ����ɫ����ʵ�ִ�λ��������Ч��
                half4 frag(v2f i) : SV_Target
                {
                    float splitAmount = Noise() * ImageBlockIntensity(i);
                    half4 col = tex2D(_MainTex, i.uv);
                    half4 finalColor;
                    finalColor.r = tex2D(_MainTex, fixed2(i.uv.x + splitAmount * randomNoise(float2(13.0, 1.0)), i.uv.y)).r;
                    finalColor.g = col.g;
                    finalColor.b = tex2D(_MainTex, fixed2(i.uv.x - splitAmount * randomNoise(float2(123.0, 1.0)), i.uv.y)).b;
                    finalColor.a = 1.0;

                    return finalColor;
                }
                ENDCG
            }
        }
}