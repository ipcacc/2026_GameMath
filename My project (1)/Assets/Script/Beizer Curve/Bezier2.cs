using UnityEngine;
using System.Collections.Generic;

public class Bezier2 : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    List<Vector3> pointPositions = new List<Vector3>();
    float timevalue = 0f;
     void Awake()
    {
        foreach (Transform pt in points)
        {
            if (pt != null)
            pointPositions.Add(pt.position);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timevalue += Time.deltaTime / 2f;
        transform.position = DeCasteljau(pointPositions, timevalue);
    }

    Vector3 DeCasteljau(List<Vector3> p, float t)
    {
        while (p.Count > 1)
        {
            int last = p.Count - 1;

            var next = new List<Vector3>(last);
            for (int i = 0; i < last; i++)
                next.Add(Vector3.Lerp(p[i], p[i + 1], t));
            p = next;
        }

        return p[0];
    }
}
