using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoving : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 30f;
    private Vector2 moveInput;
    public bool isLParry = false;
    public bool isRParry = false;

    public void OnLParry(InputValue value)
    {
        isLParry = value.isPressed;
    }

    public void OnRParry(InputValue value)
    {
        isRParry=value.isPressed;
    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void Update()
    {
        float rotation = moveInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotation, 0f);

        Vector3 moveDir = transform.forward * moveInput.y * moveSpeed * Time.deltaTime;
        transform.Translate(moveDir);
    }
}
