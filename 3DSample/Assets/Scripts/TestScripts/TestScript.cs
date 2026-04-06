using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float sprintRatio = 1f;
    public float dashSpeed = 30000f;
    private float dashCounter = 6;
    private Vector2 mouseScreenPosition;
    private Vector3 targetPosition;
    private Vector3 lastTargetPosition = new Vector3(0,0,0);
    private Vector3 prevDashPosition;
    private Vector3 dashDirection;
    private bool isMoving = false;
    private bool isSprinting = false;
    private bool isOnDash = false;
    private Vector3 normalizedVector;
    private Vector3 normalizedDashVector;


    public void OnPoint(InputValue value)
    {
        mouseScreenPosition = value.Get<Vector2>();         //마우스 위치 업데이트
    }

    public void OnRightClick(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("클릭!");
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);            //레이저 경로에 있는 모든 물체 탐색

            foreach (RaycastHit hit in hits)            //모든 물체에 반복
            {
                if (hit.collider.gameObject != gameObject)           //부딪힌 물체가 내가 아닐때만
                {
                    targetPosition = hit.point;         //Plane에 부딪힌 지점을 타겟
                    targetPosition.y = transform.position.y;
                    lastTargetPosition = targetPosition;
                    lastTargetPosition.y = targetPosition.y;
                    prevDashPosition = transform.position;
                    dashDirection = new Vector3(lastTargetPosition.x - prevDashPosition.x, lastTargetPosition.y, lastTargetPosition.z - prevDashPosition.z);

                    isMoving = true;

                    break;          //탐색 했으니 foreach 중단
                }
            }
        }
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("돌찐!!!!");
        isOnDash = true;
    }
    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }

    void Start()
    {

        //벡터의 크기는 피타고라스의 정리로 구할 수 있음
        //벡터의 정규화는 벡터의 방향을 유지하며 크기를 1로 만드는 것임
        //transform.position = new Vector3(1, 2, 3);
        //Vector3 A = new Vector3(1, 2, 3);
        //Vector3 B = new Vector3(3, 1, -2);
        //Vector3 C = A + B;
    }

    void Update()
    {
        Debug.Log(lastTargetPosition);
        if (isMoving)
        {
            Debug.Log(targetPosition);
            Vector3 direction = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);

            float sqrMagnitude = direction.x * direction.x + direction.y * direction.y +direction.z * direction.z;
            float magnitude = Mathf.Sqrt(sqrMagnitude);

            //0으로 나누면 큰일나요
            if (magnitude > 0)
            {
                normalizedVector = direction / magnitude;
            }
            else
            {
                normalizedVector = Vector3.zero;
            }


                transform.Translate(normalizedVector * moveSpeed * sprintRatio * Time.deltaTime);

            if(magnitude <= 0.3f)
            {
                isMoving = false;
                Debug.Log("도착했어요 삐립삐리뽀");
            }
        }

        if (isSprinting == true)             //좌쉬프트를 누르고 있으면 
        {
            Debug.Log("달린다!");
            sprintRatio = 2f;               //달리기 속도 배수를 2배로 설정
        }
        else
        {
            sprintRatio = 1f;     //좌쉬프를 누르지 않으면 원상복구
        }

        if (isOnDash == true)
        {
                if(lastTargetPosition == new Vector3(0,0,0))
            {
                transform.Translate(transform.forward * dashSpeed * Time.deltaTime);
            }
            else
            {
                isMoving = false; ;

                float sqrDashMagnitude = dashDirection.x * dashDirection.x + dashDirection.y * dashDirection.y + dashDirection.z * dashDirection.z;
                float dashMagnitude = Mathf.Sqrt(sqrDashMagnitude);

                //0으로 나누면 큰일나요
                if (dashMagnitude > 0)
                {
                    normalizedDashVector = dashDirection / dashMagnitude;
                }
                else
                {
                    normalizedDashVector = Vector3.zero;
                }


                transform.Translate(normalizedDashVector * dashSpeed * Time.deltaTime);
            }
                dashCounter = dashCounter - 1f;
                if (dashCounter <= 0.3f)
                {
                    isOnDash = false;
                    dashCounter = 6f;
                    Debug.Log("돌찐끗");
                }
                
            }
        }


    }

