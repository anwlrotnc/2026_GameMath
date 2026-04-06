using UnityEngine;

public class DotExample : MonoBehaviour
{
    public Transform player;
    public float viewAngle = 60f;               //시야각
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPlayer = player.position - transform.position;            //플레이어 보는 방향

        toPlayer.y = 0; 

        Vector3 forward = transform.forward;        //적의 방향 z+
        forward.y = 0;

        forward.Normalize();
        toPlayer.Normalize();

        float dot = Vector3.Dot(forward, toPlayer);     //내적

        if (dot > 0.2f)
        {
            Debug.Log("플레이어가 적 앞");
        }
        else if (dot < 0.2f)
        {
            Debug.Log("플레이어가 적 뒤");
        }
        else
        {
            Debug.Log("플레이어가 적 옆");
        }
    }
}
