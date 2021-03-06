using UnityEngine;

public class AntActionsFSM : MonoBehaviour
{
    enum AntState
    {
        Idle,
        GoingToColony,
        CollectingSugar
    }

    [SerializeField]
    Transform colonyTransform;

    [SerializeField]
    SugarController sugarController;
    [SerializeField]
    int SugarToCollect = 5;
    int SugarCollected = 0;

    const float inRange = 5;
    Seek seek;
    Sugar sugar;
    System.Action activeState;
    //System.Action previousStateExit;

    void Start()
    {
        activeState = GoToColony;
        seek = GetComponent<Seek>();
        seek.target = colonyTransform;
    }

    void Update()
    {
        activeState?.Invoke();
    }

    void GoToColony()
    {
        // state update 
        if (Vector3.Distance(colonyTransform.position, transform.position) < inRange)
        {
            SugarCollected = 0;
            sugarController.ResetSugar();
            activeState = CollectSugar;
            sugar = sugarController.GetRandomSugar();
            if (null != sugar)
            {
                seek.target = sugar.transform;
            }
        }
    }

    void CollectSugar()
    {
        // state update 
        if (null != sugar)
        {
            if (Vector3.Distance(sugar.transform.position, transform.position) < 0.5)
            {
                sugar.GetEaten();
                SugarCollected++;

                if (SugarCollected == SugarToCollect)
                {
                    activeState = GoToColony;
                    seek.target = colonyTransform;
                }
                else
                {
                    sugar = sugarController.GetRandomSugar();
                    if (null != sugar)
                    {
                        seek.target = sugar.transform;
                    }
                    else
                    {
                        activeState = GoToColony;
                        seek.target = colonyTransform;
                    }
                }
            }
        }
    }
}
