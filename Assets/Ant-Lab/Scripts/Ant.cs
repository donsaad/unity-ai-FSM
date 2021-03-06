using UnityEngine;

public class Ant : MonoBehaviour
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
    AntState antState = AntState.GoingToColony;
    Seek seek;

    void Start()
    {
        seek = GetComponent<Seek>();
        seek.target = colonyTransform;
    }

    void Update()
    {
        switch (antState)
        {
            case AntState.GoingToColony:
                if (Vector3.Distance(colonyTransform.position, transform.position) < inRange)
                {
                    SugarCollected = 0;
                    sugarController.ResetSugar();
                    antState = AntState.CollectingSugar;
                    seek.target = sugarController.GetRandomSugar().transform;
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (antState)
        {
            case AntState.CollectingSugar:
                Sugar sugar = other.gameObject.GetComponent<Sugar>();
                if (seek.target != null && sugar != null)
                {
                    if (sugar.gameObject == seek.target.gameObject)
                    {
                        sugar.GetEaten();
                        SugarCollected++;
                        if (SugarCollected == SugarToCollect)
                        {
                            antState = AntState.GoingToColony;
                            seek.target = colonyTransform;
                        }
                        else
                        {
                            sugar = sugarController.GetRandomSugar();
                            if (sugar != null)
                            {
                                seek.target = sugar.transform;
                            }
                            else
                            {
                                antState = AntState.GoingToColony;
                                seek.target = colonyTransform;
                            }
                        }
                    }
                }
                break;
        }
    }
}
