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
        if (isMoveing)
        {
            Vector3 direction = new Vector3(isMoveing.x, isMoveing.y, 0); 

            if ()
            {
                isMoveing = false;
            }
        }
    }
}
