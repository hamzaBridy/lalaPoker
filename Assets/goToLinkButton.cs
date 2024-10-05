using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goToLinkButton : MonoBehaviour
{
    public string linkText;
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => { Application.OpenURL(linkText); });
    }

}
