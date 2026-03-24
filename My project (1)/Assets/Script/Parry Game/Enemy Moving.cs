using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyMoving : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 50f;
    public float detectionRange = 8f;
    public float dashSpeed = 15f;
    public float stopDistance = 1.2f;
    public bool isDashing = false;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isDashing)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltatime);
            //이후 과제
        }
        else
        {
            //이후 과제
        }
    }
    
    void CheckParry()
    {
        PlayerMoving pc = player.GetComponent<PlayerMoving>();
        //이후 과제
    }
}
