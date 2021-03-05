using UnityEngine;

public class Fox : MonoBehaviour
{
    enum FoxState
    {
        GoingToCave,
        StayingInCave,
        ChasingChicken
    }

    FoxState foxState = FoxState.GoingToCave;

    [SerializeField]
    ChickenController chickenController;
    [SerializeField]
    Transform caveTransform;
    [SerializeField]
    float stayedInCaveTimer = 0;

    Seek seek;

    int chickenCount = 5;
    int eatenChickenCount = 0;

    const float stayInCaveTime = 5;
    const float inRange = 5;

    private void Start()
    {
        seek = GetComponent<Seek>();
        seek.target = caveTransform;
    }

    private void Update()
    {
        switch (foxState)
        {
            case FoxState.GoingToCave:
                // Exit goint to cave state
                if (Vector3.Distance(caveTransform.position, transform.position) < inRange)
                {
                    seek.target = null;

                    // Start staying in cave state
                    foxState = FoxState.StayingInCave;
                    stayedInCaveTimer = stayInCaveTime;
                }
                break;
            case FoxState.StayingInCave:
                stayedInCaveTimer -= Time.deltaTime;
                // Exit staying in cave
                if (stayedInCaveTimer <= 0)
                {
                    // Start chasing state
                    foxState = FoxState.ChasingChicken;
                    seek.target = chickenController.GetRandomChicken().transform;
                    eatenChickenCount = chickenCount;
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (foxState)
        {
            case FoxState.ChasingChicken:
                Chicken chicken = collider.gameObject.GetComponent<Chicken>();
                if (seek.target != null && chicken != null)
                {
                    if (chicken.gameObject == seek.target.gameObject)
                    {
                        chicken.GetEaten();
                        eatenChickenCount--;
                        // Exit chasing chicken state
                        if (eatenChickenCount == 0)
                        {
                            // Start cave seeking state
                            foxState = FoxState.GoingToCave;
                            seek.target = caveTransform;
                        }
                        else
                        {
                            chicken = chickenController.GetRandomChicken();
                            if (chicken == null)
                            {
                                foxState = FoxState.GoingToCave;
                                seek.target = caveTransform;
                                break;
                            }
                            seek.target = chicken.transform;
                        }
                    }
                }
                break;
        }
    }
}
