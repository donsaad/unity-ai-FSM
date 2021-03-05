using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour, ISteer
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float inRange = 5;

    public Vector3 GetForce()
    {
        Vector3 dir = target.position - transform.position;
        Vector3 velocity = dir.normalized * speed;

        if (dir.magnitude < inRange)
        {
            velocity *= dir.magnitude / inRange;
        }
        return velocity;
    }
}
