using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public (int, Transform) GetRandomChild()
    {
        int randomNum = Random.Range(0, transform.childCount);
        return (randomNum, transform.GetChild(randomNum));
    }

    public (int, Transform) GetNearestChild(Vector3 position)
    {
        int nearestChildIndex = -1;
        Transform nearestChild = null;
        float distance = float.MaxValue;

        int i = 0;
        foreach (Transform child in transform)
        {
            float newDis = Vector3.Distance(child.transform.position, position);
            if (newDis < distance)
            {
                distance = newDis;
                nearestChild = child;
                nearestChildIndex = i;
            }
            i++;
        }

        return (nearestChildIndex, nearestChild);
    }

    public (int, Transform) GetNextChild(in int index)
    {
        int newIndex = (index + 1) % transform.childCount;
        return (newIndex, transform.GetChild(newIndex));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild((i + 1) % transform.childCount).position);
        }
    }
}
