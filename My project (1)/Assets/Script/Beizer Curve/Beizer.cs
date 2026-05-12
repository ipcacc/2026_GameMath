using UnityEngine;

public class Beizer : MonoBehaviour
{
    public Transform point0;

    public Transform point1; 

    public Transform point2;

    float timevalue = 0f;
    // Update is called once per frame
    void Update()
    {
        timevalue += Time.deltaTime / 2f;
        transform.position = GetPointOnBezierCurve(point0.position, point1.position, point2.position, timevalue);
    }

    Vector3 GetPointOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 ab = Vector3.Lerp(a, b, t);
        return ab;
    }
}
