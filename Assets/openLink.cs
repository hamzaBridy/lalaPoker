using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openLink : MonoBehaviour
{
    public void OpenURL(string URL)
     {
         Application.OpenURL(URL);
     }
}
