using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPrefabScript : MonoBehaviour
{
    public bool isAvalible = false;
    public int id;
    public bool isActive;
    public APIcalls apiRef;
    public void onClick(){
        apiRef.SendSpItWrapper(id.ToString());
    }
}
