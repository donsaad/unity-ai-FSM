using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField]
    Transform fox;

    SkinnedMeshRenderer meshRenderer;
    PathFollowing pathFollowing;

    const float deathTime = 4;
    public bool IsDead { get; private set; }
    float deathTimer = 0;

    float DistanceFromFox => Vector3.Distance(fox.position, transform.position);

    System.Action activeState;
    System.Action previousStateExit;

    const float screamInterval = 2;
    float screamingTimer = 0;

    private void Start()
    {
        activeState = PathFollowing;
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        pathFollowing = GetComponent<PathFollowing>();
    }

    [ContextMenu("Die")]
    public void GetEaten()
    {
        // Previous State Exit
        previousStateExit?.Invoke();

        // Enter Dead State
        IsDead = true;
        meshRenderer.enabled = false;
        deathTimer = 0;
        activeState = Dead;
    }

    private void Update()
    {
        activeState?.Invoke();
    }

    void PathFollowingExit()
    {
        // Exiting path following
        pathFollowing.BehaviorEnabled = false;
    }

    void PathFollowing()
    {
        //Path following update

        if(DistanceFromFox < 5)
        {
            // Exit path following
            PathFollowingExit();

            // Enter scream state
            activeState = Scream;
            screamingTimer = screamInterval;
            previousStateExit = null;
        }
    }

    void Scream()
    {
        // Scream update
        screamingTimer -= Time.deltaTime;
        if (screamingTimer <= 0)
        {
            Debug.Log($"<color=red>{name} Screaming</color>");
            screamingTimer = screamInterval;
        }
        if(DistanceFromFox > 10)
        {
            // Enter Path Following
            activeState = PathFollowing;
            pathFollowing.BehaviorEnabled = true;
            previousStateExit = PathFollowingExit;
        }
    }

    void DeadExitState()
    {
        IsDead = false;
        meshRenderer.enabled = true;
    }

    void Dead()
    {
        // Dead State update
        deathTimer += Time.deltaTime;
        if (deathTimer > deathTime)
        {
            // Dead exist state
            DeadExitState();
            // Path following enter
            pathFollowing.GetRandomTarget();
            pathFollowing.BehaviorEnabled = true;
            activeState = PathFollowing;
        }
    }
}
