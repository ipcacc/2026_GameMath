using UnityEngine;

public class Beizer1 : MonoBehaviour
{
    public Transform point0;

    public Transform point1; 

    public Transform point2;

    public Transform point3;

    float timevalue = 0f;
    // Update is called once per frame
    void Update()
    {
        timevalue += Time.deltaTime / 2f;
        transform.position = GetPointOnBezierCurve(point0.position, point1.position, point2.position, point3.position, timevalue);
    }

    Vector3 GetPointOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 abc = Vector3.Lerp(ab, bc, t);
        return abc;
    }
}
