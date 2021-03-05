using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SugarController : MonoBehaviour
{
    List<Sugar> sugarList;

    private void Start()
    {
        sugarList = GetComponentsInChildren<Sugar>().ToList();
    }

    public Sugar GetRandomSugar()
    {
        return sugarList.FirstOrDefault(s => !s.IsCollected);
    }
    public void ResetSugar()
    {
        foreach (var item in sugarList)
        {
            item.IsCollected = false;
        }
    }
}
