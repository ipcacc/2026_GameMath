using UnityEngine;

public class Lerpmove : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    [SerializeField] private float duration = 2f;
    [SerializeField] private float t = 0f;
    
    void Update()
    {
        t += Time.deltaTime / duration;
        transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
        t = Mathf.PingPong(Time.time / duration, 1f);
    }
}
