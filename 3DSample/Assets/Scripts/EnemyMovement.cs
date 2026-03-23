using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float viewAngle = 60f;       //시야각
    public float viewDistance = 5;      //시야범위
    void Start()
    {
        
    }

    void Update()
    {
        //Vector3 PlayerPos = new Vector3(player.position.x, 0, player.position.z);
        float xDistance = transform.position.x - player.position.x;
        float zDistance = transform.position.z - player.position.z;
        float playerDistance = Mathf.Sqrt((xDistance * xDistance) + (zDistance * zDistance));
        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;
        float dot = Vector3.Dot(forward, toPlayer);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;      //내적을 각도로 변환

        if(angle < viewAngle / 2 )
        {
            if(playerDistance <= viewDistance)
            {
                Debug.Log(playerDistance);
                transform.localScale = Vector3.one * 2;
            }
        }
        if(angle >= viewAngle /2 || playerDistance >= viewDistance)
        {
            transform.localScale = Vector3.one;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 forward = transform.forward * viewDistance;

        //왼쪽 시야 경계
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * forward;
        //오른쪽 시야 경계
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * forward;

        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawRay(transform.position, rightBoundary);

        //캐릭터 앞쪽 방향
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, forward);
    }
}
