using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 mouseScreenPosition;
    private Vector3 targetPosition;
    private bool isMoveing = false;

    public void OnPoint(InputValue value)
    {
        mouseScreenPosition = value.Get<Vector2>();
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
                    isMoveing = true;

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

            float sqrMagnitude = direction.x * direction.x + direction.y * direction.y + direction.z * direction.z;
            float magnitude = Mathf.Sqrt(sqrMagnitude);

            Vector3 normalizedVector;

            if (magnitude > 0)
                normalizedVector = direction / magnitude;
            else
                normalizedVector = Vector3.zero;

            transform.Translate(normalizedVector * moveSpeed * Time.deltaTime);

            if (magnitude < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}
