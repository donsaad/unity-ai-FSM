using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour, ISteer
{
    [SerializeField]
    float range = 5;
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    float speed = 5;

    public bool BehaviorEnabled = true;

    [SerializeField]
    Vector3 target;

    public Vector3 GetForce()
    {
        if (!BehaviorEnabled) return Vector3.zero;

        if((target - transform.position).magnitude  < 1)
        {
            target = GetRandomPoint();
        }

        Vector3 dir = target - transform.position;
        Vector3 velocity = dir.normalized * speed;

        return velocity;
    }

    Vector3 GetRandomPoint()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * range;
            RaycastHit hit;
            Ray ray = new Ray(randomPoint, -transform.up);

            if (Physics.Raycast(ray, out hit, float.MaxValue, mask))
            {
                return hit.point;
            }
        }

        return transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(target, 1);
    }
}
