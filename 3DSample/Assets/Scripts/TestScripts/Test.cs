using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 mouseScreenPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    Vector3 normalizedVector;


    public void OnMove(InputValue value)
    {
        //moveInput = value.Get<Vector2>();
    }

    //---------------------------------------------------------

    public void OnPoint(InputValue value)
    {
        mouseScreenPosition = value.Get<Vector2>();         //마우스 위치 업데이트
    }

    public void Onclick(InputValue value)
    {
        if(value.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);            //레이저 경로에 있는 모든 물체 탐색

            foreach (RaycastHit hit in hits)            //모든 물체에 반복
            {
                if(hit.collider.gameObject != gameObject)           //부딪힌 물체가 내가 아닐때만
                {
                    targetPosition = hit.point;         //Plane에 부딪힌 지점을 타겟
                    targetPosition.y = transform.position.y;
                    isMoving = true;

                    break;          //탐색 했으니 foreach 중단
                }
            }
        }
    }
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        Vector3 direction = new Vector3(moveInput.x, moveInput.y, 0); //.normalized 딸깍으로 가능한데 안하고 해보기

        float sqrMagnitude = direction.x * direction.x + direction.y * direction.y + direction.z * direction.z;
        float magnitude = Mathf.Sqrt(sqrMagnitude);

        //0으로 나누는 참사 방지

        if (magnitude > 0)
        {
            normalizedVector = direction / magnitude;
        }
        else
            normalizedVector = Vector3.zero;

        transform.Translate(normalizedVector * moveSpeed * Time.deltaTime);


        transform.Translate(direction * moveSpeed * Time.deltaTime);

        //--------------------------------------실습 4--------------
        if (isMoving)
        {

        }

    }
}
