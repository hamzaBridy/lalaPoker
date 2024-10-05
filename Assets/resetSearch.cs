using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetSearch : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        didnotSearchYet.searched=false;
    }
    public void setSearch(){
        didnotSearchYet.searched=true;
    }
}
