using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AUtoRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 45 * Time.deltaTime, 0);
    }
}
