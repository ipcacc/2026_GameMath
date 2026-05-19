using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicTest : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * 10f);
    }

    // Update is called once per frame
    void Update()
    {
        speed = rb.linearVelocity.magnitude;
    }
}
