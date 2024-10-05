using System.Collections.Generic;
using UnityEngine;

public static class DestroyListElements
{
    // Static function to destroy all elements in a list
    public static void DestroyElements<T>(List<T> elements) where T : Object
    {
        foreach (var element in elements)
        {
            if (element != null)
            {
                Object.Destroy(element);
            }
        }
        elements.Clear(); // Clear the list after destroying all elements
    }
}
