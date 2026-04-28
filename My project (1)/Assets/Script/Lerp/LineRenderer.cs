using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class PredLineRenderer : MonoBehaviour
{
    public Transform startPos;
    Transform endPos;
    [Range(1f, 5f)] public float extend = 1.5f;
    private LineRenderer lr;

    public CameraSlerp cam;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.widthMultiplier = 0.05f;
        lr.material = new Material(Shader.Find("Unlit/Color"))
        {
            color = Color.red
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPos || !endPos) return;
        Vector3 a = startPos.position;
        Vector3 b = endPos.position;
        Vector3 pred = Vector3.LerpUnclamped(a, b, extend);
        lr.SetPosition(0, a);
        lr.SetPosition(1, pred);
    }

    public void OnRightClick(InputValue value)
    {
        if (value.isPressed) return;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                endPos = hit.collider.transform;
                startPos = transform;    
                cam.target = endPos;
            }
        }
        else
        {
            startPos = null;
            endPos = null;
            cam.target = null;
            cam.transform.rotation = cam.originRotation;
            origin();
        }
    }

    void origin()
    {
        startPos = transform;
    }
}
