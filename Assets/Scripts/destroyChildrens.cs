using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyChildrens : MonoBehaviour
{
    public void DestroyAllChildren()
    {
        // Iterate through all the children of the current game object
        foreach (Transform child in transform)
        {
            // Destroy each child
            Destroy(child.gameObject);
        }
    }
    void OnEnable()
    {
        DestroyAllChildren();
    }
}
