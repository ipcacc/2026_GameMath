using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaserEnemy : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 50f;
    public float detectionRange = 8f;
    public float dashSpeed = 15f;
    public float stopDistance = 1.2f;
    public float viewAngle = 60f;

    private bool isDashing = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (!isDashing)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

            Vector3 dir = (player.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dir);

            if (distance <= detectionRange && angle <= viewAngle * 0.5f)
            {
                isDashing = true;
            }
        }
        else
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * dashSpeed * Time.deltaTime;

            transform.rotation = Quaternion.LookRotation(dir);

            if (distance <= stopDistance)
            {
                CheckParry();
            }
        }
    }

    void CheckParry()
    {
        PlayerMoving pc = player.GetComponent<PlayerMoving>();

        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        Vector3 cross = Vector3.Cross(transform.forward, dirToPlayer);

        bool isLeft = cross.y > 0;

        if (isLeft && pc.isLParry)
        {
            Destroy(gameObject);
        }
        else if (!isLeft && pc.isRParry)
        {
            Destroy(gameObject);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
