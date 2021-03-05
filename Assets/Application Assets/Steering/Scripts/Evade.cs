using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : MonoBehaviour, ISteer
{
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    float rayDis = 2;
    [SerializeField]
    float speed = 10;
    Vector3 velocity = Vector3.zero;
    public Vector3 GetForce()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + new Vector3(0, 1, 0), transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDis);

        if (Physics.Raycast(ray, out hit, rayDis, mask))
        {
            Vector3 dir = (transform.position - hit.point).normalized + (hit.point - hit.transform.position).normalized;
            velocity = dir * speed;
        }
        else if (velocity.magnitude > 0)
        {
            velocity -= velocity.normalized * Time.fixedDeltaTime * 20;
        }

        return velocity;
    }
}
