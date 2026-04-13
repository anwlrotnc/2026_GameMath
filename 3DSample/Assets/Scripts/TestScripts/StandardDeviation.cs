using System.Linq;
using UnityEngine;

public class StandardDeviation : MonoBehaviour
{
    public int n = 1000;                        //샘플 수
    public int[] sampleRange = { 0, 1 };        //범위 설정. 요소0 최소 ~ 요소1 최대
    public float[] gaussianRange = {0f,0f };        //0 평균 1 표준편차
    void Start()
    {

    }
    public void OnButtonClick()
    {
        float gaussianResult;
        gaussianResult = GenerateGaussian(gaussianRange[0], gaussianRange[1]);
        Debug.Log(gaussianResult);

    }
    public float GenerateGaussian(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value;     //0보다 큰 난수
        float u2 = 1.0f - Random.value;     //0보다 큰 난수

        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);          //표준 정규 분포
        
        return mean + stdDev * randStdNormal;           //원하는 평균과 표준편차로 변환
                                                                                                            
    }
    void Update()
    {
        
    }
    void RunStdDeviation()
    {
        //int n = 100000;       //샘플 수
        float[] samples = new float[n];
        for (int i = 0; i < n; i++)
        {
            samples[i] = Random.Range(sampleRange[0], sampleRange[1]);
        }

        float mean = samples.Average();
        float sumOfSquares = samples.Sum(x => Mathf.Pow(x - mean, 2));
        float stdDev = Mathf.Sqrt(sumOfSquares / n);

        Debug.Log($"평균 : {mean}, 표준편차 : {stdDev}");
    }
}
