using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Zombie : MonoBehaviour
{
    [HideInInspector]
    public Wander wander;
    [HideInInspector]
    public Flee flee;

    [SerializeField]
    Transform fox;
    Animator zombieAnimator;

    public void EscapeFromFox()
    {
        flee.target = fox;
    }

    private void Start()
    {
        wander = GetComponent<Wander>();
        flee = GetComponent<Flee>();
        zombieAnimator = GetComponent<Animator>();
        zombieAnimator.GetBehaviours<ZombieState>().ToList().ForEach(zs => zs.zombie = this);
    }

    private void Update()
    {
        zombieAnimator.SetFloat("distanceFromFox", Vector3.Distance(transform.position, fox.position));
    }
}
