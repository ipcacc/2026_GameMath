using UnityEngine;

public class SolorSystem : MonoBehaviour
{
    public float angle = 0f;
    public float speed = 1f;
    public float radius = 5f;

    public Transform center;
   
    void Update()
    {
        angle += speed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        transform.position = center.position + new Vector3(x, 0, z);
    }
}
