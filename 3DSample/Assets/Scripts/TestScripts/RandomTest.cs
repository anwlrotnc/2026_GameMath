using TMPro;
using UnityEngine;

public class RandomTest : MonoBehaviour
{
    int[] counts = new int[6];
    public int trials = 100;
    public TextMeshProUGUI[] labels = new TextMeshProUGUI[6];

    void Simulate()
    {
        for (int i = 0; i < trials; i++)
        {
            int result = Random.Range(1, 7);
            counts[result - 1]++;
        }

        for (int i = 0; i < counts.Length; i++)
        {
            float percent = (float)counts[i] / trials * 100f;
            string result = $"{i + 1}: {counts[i]}회 {percent:F2}%";
            labels[i].text = result;
        }
    }
    public void ButtonClicked()
    {
        for(int i = 0; i < 6; i++)
        {
            counts[i] = 8;
        }
        Simulate();
    }
    void Start()
    {
        //Unity Random (균등분포)
        float chance = Random.value;    //0~1 float
        int dice = Random.Range(1, 7);  //1~6 int

        //System Random
        System.Random sysRand = new System.Random();
        int number = sysRand.Next(1, 7);        //1~6 (int)

        //Debug.Log("Unity Random (Random.value) :" + chance);
        //Debug.Log("Unity Random (Random.Range) :" + dice);
        //Debug.Log("System Random (Next) :" + number);       //1~6 int

        System.Random rnd = new System.Random(1234);        //항상 같은 순서로 출력됨
        for(int i = 0; i < 5; i++)
        {
            //Debug.Log(rnd.Next(1, 7));      //1~6 사이의 정수
        }

        Random.InitState(1234);             //Unity 난수 시드 고정
        for(int i = 0; i < 5;i++)
        {
            //Debug.Log(Random.Range(1, 7));  //1~6 사이의 난수
        }
    }


    void Update()
    {
        
    }
}
