using UnityEngine;

public class OrbitRevolutionCtr : MonoBehaviour
{
    //GameObject SUN;
    public Transform  RevolutionCenter;                      //공전의 주인
    public float speed = 2f;                  //공전 속도
    public float radius = 5f;                 //공전하는 거리(공전궤도의 반지름)
    private float angle = 0f;


    void Start()
    {
        
    }

    void Update()
    {
        //시간에 따른 라디안 계산
        //라디안은 '호'의 길이로 각도를 나타내는 방법임. (180도 = 1파이 라디안)
        //어느 한 원 위의 점이 원점을 중심으로 반지름의 길이만큼 한 방향으로 움직였을 때 대응하는 각의 크기를 1라디안이라고 정의함
        angle += speed * Time.deltaTime;                    //공전은 두 천체가 "일정한" 주기를 가지고 도는 운동이기 때문에 Time.deltaTime을 일정하게 곱함

        float x = Mathf.Cos(angle) * radius;                //Mathf.삼각함수()는 반지름이 1인 원을 기준으로 잡기 때문에 공전거리를 곱해줌                 
        float z = Mathf.Sin(angle) * radius;                //"공전"해야 하기 때문에 라디안을 각도로 변환하지 않고 ('-1' ~ '1') * (공전궤도의 반지름) 해서 같은 지점을 왔다갔다 하게 만들기. 

        //공전 중심의 위치에 값을 더하기
        transform.position = new Vector3(RevolutionCenter.position.x + x, transform.position.y, RevolutionCenter.position.z + z);
    }
}
