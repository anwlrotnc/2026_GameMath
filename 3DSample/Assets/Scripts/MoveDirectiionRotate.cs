using UnityEngine;
using UnityEngine.InputSystem;
public class MoveDirectiionRotate : MonoBehaviour
{
    private Vector2 moveInput;

    public void OnMove(InputValue value)                //WASD ´©¸£¸é
    {
        moveInput = value.Get<Vector2>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(moveInput.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

        }
    }
}
