Shader "Imageblock"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}  // 主纹理，用于处理的图像
        _Amplitude("Amplitude", Range(-1, 0)) = -0.15  // 线条错位的振幅，范围在-1到0之间
        _Amount("Amount", Range(-5, 5)) = 0.5  // 整体错位的强度，范围在-5到5之间
        _BlockSize("Block Size", Range(0, 1)) = 0.05  // 线条块的大小，范围在0到1之间
        _Speed("Speed", Range(0, 100)) = 10  // 线条错位的速度，范围在0到100之间
        _BlockPow("Block Size Pow", Vector) = (3, 3, 0, 0)  // 线条块的幂次，用于控制线条块的形状
    }
        SubShader
        {
            // 不进行剔除和深度测试
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

                // 顶点着色器，将顶点位置和UV传递到片段着色器
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

                // 随机噪声函数，接受一个二维向量作为种子
                inline float randomNoise(float2 seed)
                {
                    return frac(sin(dot(seed * floor(_Time.y * _Speed), float2(17.13, 3.71))) * 43758.5453123);
                }

                // 计算线条错位的强度
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

                // 计算每个像素点线条错位的强度
                float ImageBlockIntensity(v2f i)
                {
                    float2 size = lerp(1, _MainTex_TexelSize.xy, 1 - _BlockSize);
                    size = floor((i.uv) / size);
                    float noiseBlock = randomNoise(size);
                    float displaceNoise = pow(noiseBlock, _BlockPow.x) * pow(noiseBlock, _BlockPow.y);
                    return displaceNoise;
                }

                // 片段着色器，实现错位线条故障效果
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