using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transLogDes : MonoBehaviour
{
    public GameObject transLog;
    void Start()
    {
        
    }
    public void onClick() {
        transLog.GetComponent<destroyChildrens>().DestroyAllChildren();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
