using System.Linq;
using UnityEngine;

public class AntAnimatorFSM : MonoBehaviour
{

    public Transform colonyTransform;
    Animator animator;

    public SugarController sugarController;
    public const float inRange = 5;
    [HideInInspector]
    public Seek seek;
    public bool SugarHit { get; set; }

    void Start()
    {
        seek = GetComponent<Seek>();
        animator = GetComponent<Animator>();
        animator.GetBehaviours<AntState>().ToList().ForEach(x => x.ant = this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Sugar sugar = other.gameObject.GetComponent<Sugar>();
        if (seek.target != null && sugar != null)
        {
            if (sugar.gameObject == seek.target.gameObject)
            {
                if (!sugar.IsCollected)
                {
                    sugar.GetEaten();
                    SugarHit = true;
                }
            }
        }

    }
}
