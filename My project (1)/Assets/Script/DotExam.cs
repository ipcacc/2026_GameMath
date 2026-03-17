using UnityEngine;

public class DotExqm : MonoBehaviour
{
    public Transform Player;

    private void Update()
    {
        Vector3 toPlayer = Player.position - transform.position;
        toPlayer.y = 0;

        Vector3 forward = transform.forward;
        forward.y = 0f;

        forward.Normalize();
        toPlayer.Normalize();

        float dot = Vector3.Dot(forward, toPlayer);

        if (dot > 0f)
        {
            Debug.Log("플레이어가 적의 앞쪽에 있음");
        }
        else if (dot < 0f)
        {
            Debug.Log("플레이어가 적의 뒤쪽에 있음");
        }
        else
        {
            Debug.Log("플레이어가 적의 옆쪽에 있음");
        }

    }
}
