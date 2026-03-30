using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCtr : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 30f;
    private Vector2 moveInput;
    public bool isLeftParraying = false;
    public bool isRightParraying = false;
    
    //왼쪽 패링 입력
    public void OnLeftParry(InputValue value)
    {
        isLeftParraying = value.isPressed;
    }

    //오른쪽 패링 입력
    public void OnRightParry(InputValue value)
    {
        isRightParraying = value.isPressed;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void Start()
    {
        
    }


    void Update()
    {
        float rotation = moveInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotation, 0f);

        Vector3 moveDir = transform.forward * moveInput.y * moveSpeed * Time.deltaTime;
        transform.Translate(moveDir);
    }
}
