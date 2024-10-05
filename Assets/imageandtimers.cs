using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageandtimers : MonoBehaviour
{
    [SerializeField] GameObject timers, avatars;
    void OnEnable()
    {
        timers.SetActive(false);
        avatars.SetActive(true);
    }
}
