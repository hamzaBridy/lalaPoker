using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class dealingStart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dealingCard;
    public GameObject dealer;
    public List<GameObject> destinationLocations;
    private float duration=0.5f;
    // Update is called once per frame
   
    public void deal(){
        float i=0;
        StartCoroutine(dealingAnimation(destinationLocations.Count*0.8f));
        foreach (GameObject item in destinationLocations)
        {
            if (destinationLocations!=null)
            {
                
            
            StartCoroutine(dealing(item,i));
            StartCoroutine(dealing(item,i+(destinationLocations.Count*0.4f)));
            i+=0.4f;
            }
        }
    }
     IEnumerator dealingAnimation(float Dealy){
        
        dealer.GetComponent<SkeletonAnimation>().AnimationName="Throwing";
        yield return new WaitForSeconds(Dealy);
        dealer.GetComponent<SkeletonAnimation>().AnimationName="Idle";
     }
        IEnumerator dealing(GameObject destinationTransform,float Dealy)
    {

        yield return new WaitForSeconds(Dealy);
        


            GameObject cPf=Instantiate(dealingCard);
            cPf.transform.SetParent(transform, false);
            float startTime = Time.time;
            Vector3 startingPos = cPf.transform.position;
            Vector3 targetPos = destinationTransform.transform.position;
            float distance = Vector3.Distance(startingPos, targetPos);
            while (Time.time - startTime < duration) 
            {
                float t = (Time.time - startTime) / duration; // Normalized time between 0 and 1

                // Use lerp to interpolate between starting and target positions
                cPf.transform.position = Vector3.Lerp(startingPos, targetPos, t);

                yield return null;
            }
            cPf.transform.position = targetPos;
            Destroy(cPf);


        
    //    GetComponent<UnityEngine.UI.Image>().color=new Color(1f, 0f, 0f, 0.5f);
    }
}
