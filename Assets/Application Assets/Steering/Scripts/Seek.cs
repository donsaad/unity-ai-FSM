using UnityEngine;

public class Seek : MonoBehaviour, ISteer
{
    public Transform target;
    [SerializeField]
    float speed = 5;

    SteeringBase _base;
    public virtual Vector3 GetForce()
    {
        if (target == null) return Vector3.zero;

        Vector3 dir = target.transform.position - transform.position;
        Vector3 velocity = dir.normalized * speed;

        return velocity;
    }
}
