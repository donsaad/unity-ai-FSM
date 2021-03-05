using UnityEngine;

public class PathFollowing : MonoBehaviour, ISteer
{
    [SerializeField]
    float speed = 5;
    [SerializeField]
    Path path;
    [SerializeField]
    float inRange = 1;

    int index = -1;

    Transform target;

    public bool BehaviorEnabled;

    public void GetRandomTarget()
    {
        (index, target) = path.GetRandomChild();
        transform.position = target.position;
    }

    public Vector3 GetForce()
    {
        if (!BehaviorEnabled) return Vector3.zero;

        Vector3 velocity = Vector3.zero;

        if (index < 0)
        {
            (index, target) = path.GetNearestChild(transform.position);
        }

        else
        {
            Vector3 dir = target.position - transform.position;
            velocity = dir.normalized * speed;

            if(dir.magnitude < inRange)
            {
                (index, target) = path.GetNextChild(index);
            }
        }

        return velocity;
    }
}
