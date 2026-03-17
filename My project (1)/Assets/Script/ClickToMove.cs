using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;

    private Vector2 mouseScreenPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isSprinting = false;

    public void OnPoint(InputValue value)
    {
        mouseScreenPosition = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }

    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach(RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != gameObject)
                {
                    targetPosition = hit.point;
                    targetPosition.y = transform.position.y;
                    isMoving = true;

                    break;
                }
            }
        }
    }


    void Update()
    {
        if (isMoving)
        {
            Vector3 direction = targetPosition - transform.position;

            float sqrMagnitude =direction.x * direction.x + direction.y * direction.y + direction.z * direction.z;
            float magnitude = Mathf.Sqrt(sqrMagnitude);

            Vector3 normalizedVector;

            if (magnitude > 0)
                normalizedVector = direction / magnitude;
            else
                normalizedVector = Vector3.zero;

            float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

            transform.Translate(normalizedVector * currentSpeed * Time.deltaTime);

            if (magnitude < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}
