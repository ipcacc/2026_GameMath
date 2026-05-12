using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingBezier : MonoBehaviour
{
    public GameObject bullet;
    public Transform target;
    void OnAttack()
    {
        Shooting();
    }

    void Shooting()
    {
       for (int i = 0; i < 100; i++)
       {
           RandomBezier bezier = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<RandomBezier>();
            bezier.p0 = this.transform;
            bezier.p3 = target;
            bezier.StartShooting();
       }   
    }   
}
