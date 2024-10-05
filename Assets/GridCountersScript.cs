using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridAspectContentChange : MonoBehaviour
{
    void Update()
    {
        float AspectRatio = (float)Screen.width / (float)Screen.height;
        if (AspectRatio < 1.4f)
        {
            GetComponent<GridLayoutGroup>().constraintCount = 1;
        }
        else
        {
            GetComponent<GridLayoutGroup>().constraintCount = 2;

        }
    }
}
