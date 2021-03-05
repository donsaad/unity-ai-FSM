using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    List<Chicken> chickens;

    private void Start()
    {
        chickens = GetComponentsInChildren<Chicken>().ToList();
    }

    public Chicken GetRandomChicken()
    {
        return chickens.FirstOrDefault(c => !c.IsDead);
    }
}
