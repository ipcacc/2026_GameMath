using UnityEngine;

public class FOV : MonoBehaviour
{
    public Transform player;

    public float viewAngle = 60f;

    void Update()
    {
        Vector3 toplayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        float angle = Mathf.Acos(DotProduct(forward, toplayer)) * Mathf.Rad2Deg;

        if (angle < viewAngle / 2)
        {
            transform.localScale = Vector3.one * 2;
            Debug.Log("플레이어가 시야 안에 있음");
        }
    }

  //float dot = Vector3.Dot(forward, toplayer);
    float DotProduct(Vector3 forward, Vector3 toplayer)
    {
       return forward.x * toplayer.x + forward.y * toplayer.y + forward.z * toplayer.z;
    }
}
