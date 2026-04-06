using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 50f;
    private Vector2 moveInput;
    public bool isLeftParrying = false;
    public bool isRightParrying = false;
    public GameObject UI;
    public GameObject Destination;
    

    //왼쪽 패링 입력
    public void OnLeftParry(InputValue value)
    {
        isLeftParrying = value.isPressed;
    }

    //오른쪽 패링 입력
    public void OnRightParry(InputValue value)
    {
        isRightParrying = value.isPressed;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Update()
    {
        float rotation = moveInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotation, 0f);

        Vector3 moveDir = moveInput.y * moveSpeed * Time.deltaTime * transform.forward;
        transform.position += moveDir;
        if(Vector3.Distance(transform.position, Destination.transform.position) < 0.5f)
        {
            Time.timeScale = 0;
            UI.SetActive(true);
        }
    }
}
