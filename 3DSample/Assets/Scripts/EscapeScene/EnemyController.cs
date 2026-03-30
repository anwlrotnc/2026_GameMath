using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 50f;
    public float detectionRange = 8f;
    public float dashSpeed = 15f;
    public float stopDistance = 1.2f;
    public float viewAngle = 60f;
    public bool isDashing = false;
    float playerDistance;
    float sideChecker;
    private Rigidbody rb;
    Renderer monsterColor;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        monsterColor = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {

        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;
        playerDistance = Vector3.Distance(player.position, transform.position);

        Vector3 A = player.position;
        Vector3 B = transform.position;
        Vector3 crossProduct = new Vector3(A.y * B.z - A.z * B.y, A.z * B.x - A.x * B.z, A.x * B.y - A.y * B.x);            //외적 구하기
        sideChecker = crossProduct.y;
        float dot = Vector3.Dot(forward, toPlayer);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;      //내적 -> 각도

        if (!isDashing)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            
            if(angle < viewAngle / 2)
            {
                if(playerDistance <= detectionRange)
                {
                    isDashing = true;
                }
            }
        }
        else
        {
            monsterColor.material.color = Color.purple;
            transform.LookAt(player);                                                       //플레이어를 바라보며
            transform.Translate(new Vector3(0, 0, 1) * dashSpeed * Time.deltaTime);         //몬스터 기준 앞 방향으로 이동
            
            if(playerDistance <= 1)
            {
                CheckParry();
            }
        }
    }

    void CheckParry()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        if (sideChecker < 0.1f)                         //플레이어가 왼쪽에 있으면
        {
            if(pc.isLeftParrying == true)
            {
                Destroy(gameObject);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if(sideChecker > 0.1f)                     //플레이어가 오른쪽에 있으면
        {
            if(pc.isRightParrying == true)
            {
                Destroy(gameObject);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
