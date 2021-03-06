using UnityEngine;

public class Sugar : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public bool IsCollected { get; set; }

    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        IsCollected = false;
    }


    void Update()
    {
        if (IsCollected)
        {
            meshRenderer.material.color = Color.red;
        }
        else
        {
            meshRenderer.material.color = Color.white;
        }
    }


    public void GetEaten()
    {
        IsCollected = true;
    }

}
