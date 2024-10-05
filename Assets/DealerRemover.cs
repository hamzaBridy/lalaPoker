using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerRemover : MonoBehaviour
{
    public Transform dealer;
    // Start is called before the first frame update
    private void OnEnable()
    {   float x = dealer.localPosition.x;
        float y = dealer.localPosition.y;
        dealer.localPosition = new Vector3(x, y, 0f);
    }
    private void OnDisable()
    {
        float x = dealer.localPosition.x;
        float y = dealer.localPosition.y;
        dealer.localPosition = new Vector3(x, y, -1f);
    }
}
