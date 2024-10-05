using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GIFsList : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject contentHolder;
    public GameObject contentHolder2;
    public GameObject prefab; 
    public List<bool> isEmo;
     public List<Sprite> DanceMan;
     public List<Sprite> gif1;
     public List<Sprite> gif2;
     public List<Sprite> gif3;
     public List<Sprite> gif4;
     public List<Sprite> gif5;
     public List<Sprite> gif6;
     public List<Sprite> gif7;
     public List<Sprite> gif8;
     public List<Sprite> gif9;
     public List<Sprite> gif10;
     public List<Sprite> gif11;
     public List<Sprite> gif12;
     public List<Sprite> gif13;
     public List<Sprite> gif14;
     public List<Sprite> gif15;
     public List<Sprite> gif16;
     public List<Sprite> gif17;
     public List<Sprite> gif18;
     public List<Sprite> gif19;
     public List<Sprite> gif20;
     public List<Sprite> gif21;
     public List<Sprite> gif22;
     public List<Sprite> gif23;
     public List<Sprite> gif24;
     public List<Sprite> gif25;
     public List<Sprite> gif26;
     public List<Sprite> gif27;
     public List<Sprite> gif28;
     public List<Sprite> gif29;
     public List<Sprite> gif30;
     public List<Sprite> gif31;
     public List<Sprite> gif32;
     public List<Sprite> gif33;
     public List<Sprite> gif34;
     public List<Sprite> gif35;
     public List<Sprite> gif36;
     public List<Sprite> gif37;
     public List<Sprite> gif38;
     public List<Sprite> gif39;
     public List<Sprite> gif40;
     public List<Sprite> gif41;
     public List<Sprite> gif42;
     public List<Sprite> gif43;
     public List<Sprite> gif44;
     public List<Sprite> gif45;
     public List<Sprite> gif46;
     public List<Sprite> gif47;
     public List<Sprite> gif48;
     public List<Sprite> gif49;
     public List<Sprite> gif50;
     public List<Sprite> gif51;
     public List<Sprite> gif52;
     public List<Sprite> gif53;
     public List<Sprite> gif54;
     public List<Sprite> gif55;
     public List<Sprite> gif56;
     public List<Sprite> gif57;
     public List<Sprite> gif58;
     public List<Sprite> gif59;
     public List<Sprite> gif70;
     public List<Sprite> gif71;
     public List<Sprite> gif72;
     public List<Sprite> gif73;
     public List<Sprite> gif74;
     public List<Sprite> gif75;
     public List<Sprite> gif76 ;
     public List<Sprite> gif77;
     public List<Sprite> gif78;
     public List<Sprite> gif79;
     public List<Sprite> gif80;
     public List<Sprite> gifn1;
     public List<Sprite> gifn2;
     public List<Sprite> gifn3;
     public List<Sprite> gifn4;
     public List<Sprite> gifn5;
     public List<Sprite> gifn6;
     public List<Sprite> gifn7;
     public List<Sprite> gifn8;
     public List<Sprite> gifn9;
     public List<Sprite> gifn10;
        public List<Sprite> gifn11;
        public List<Sprite> gifn12;
        public List<Sprite> gifn13;
        public List<Sprite> gifn14;
        public List<Sprite> gifn15;
        public List<Sprite> gifn16;
        public List<Sprite> gifn17;
        public List<Sprite> gifn18;
        public List<Sprite> gifn19;
        public List<Sprite> gifn20;
    public List<Sprite> gifn21;
    public List<Sprite> gifn22;
    public List<Sprite> gifn23;
    public List<Sprite> gifn24;
    public List<Sprite> gifn25;
    public List<Sprite> gifn26;
    public List<Sprite> gifn27;
    public List<Sprite> gifn28;
    public List<Sprite> gifn29;
    public List<Sprite> gifn30;
    public List<Sprite> gifn31;
    public List<Sprite> gifn32;
    public List<Sprite> gifn33;
    public List<Sprite> gifn34;
    public List<Sprite> gifn35;
    public List<Sprite> gifn36;
    public List<Sprite> gifn37;
    public List<Sprite> gifn38;
    public List<Sprite> gifn39;
    public List<Sprite> gifn40;
    public List<Sprite> gifn41;
    public List<Sprite> gifn42;
    public List<Sprite> gifn43;
    public List<Sprite> gifn44;
    public List<Sprite> gifn45;
    public List<Sprite> gifn46;
    public List<Sprite> gifn47;
    public List<Sprite> gifn48;
    public List<Sprite> gifn49;
    public List<Sprite> gifn50;
    public List<Sprite> gifn51;
    public List<Sprite> gifn52;
    public List<Sprite> gifn53;
    public List<Sprite> gifn54;
    public List<Sprite> gifn55;
    public List<Sprite> gifn56;

    public List<List<Sprite>> allGifLists = new List<List<Sprite>>();
     public float frameTime = 0.1f;
    void Start()
    {
        isEmo.Add(false);
        // Add all your individual lists to the main list
        allGifLists.Add(DanceMan);
        isEmo.Add(false);
        allGifLists.Add(gif1);
        isEmo.Add(false);
        allGifLists.Add(gif2);
        isEmo.Add(true);
        allGifLists.Add(gif3);
        isEmo.Add(false);
        allGifLists.Add(gif4);
        isEmo.Add(true);
        allGifLists.Add(gif5);
        isEmo.Add(true);
        allGifLists.Add(gif6);
        isEmo.Add(true);
        allGifLists.Add(gif7);
        isEmo.Add(true);
        allGifLists.Add(gif8);
        isEmo.Add(false);
        allGifLists.Add(gif9);
        isEmo.Add(true);
        allGifLists.Add(gif10);
        isEmo.Add(true);
        allGifLists.Add(gif11);
        isEmo.Add(true);
        allGifLists.Add(gif12);
        isEmo.Add(false);
        allGifLists.Add(gif13);
        isEmo.Add(false);
        allGifLists.Add(gif14);
        isEmo.Add(false);
        allGifLists.Add(gif15);
        isEmo.Add(false);
        allGifLists.Add(gif16);
        isEmo.Add(false);
        allGifLists.Add(gif17);
        isEmo.Add(false);
        allGifLists.Add(gif18);
        isEmo.Add(true);
        allGifLists.Add(gif19);
        isEmo.Add(true);
        allGifLists.Add(gif20);
        isEmo.Add(true);
        allGifLists.Add(gif21);
        isEmo.Add(false);
        allGifLists.Add(gif22);
        isEmo.Add(false);
        allGifLists.Add(gif23);
        isEmo.Add(true);
        allGifLists.Add(gif24);
        isEmo.Add(true);
        allGifLists.Add(gif25);
        isEmo.Add(false);
        allGifLists.Add(gif26);
        isEmo.Add(false);
        allGifLists.Add(gif27);
        isEmo.Add(false);
        allGifLists.Add(gif28);
        isEmo.Add(true);
        allGifLists.Add(gif29);
        isEmo.Add(true);
        allGifLists.Add(gif30);
        isEmo.Add(false);
        allGifLists.Add(gif31);
        isEmo.Add(false);
        allGifLists.Add(gif32);
        isEmo.Add(false);
        allGifLists.Add(gif33);
        isEmo.Add(false);
        allGifLists.Add(gif34);
        isEmo.Add(false);
        allGifLists.Add(gif35);
        isEmo.Add(false);
        allGifLists.Add(gif36);
        isEmo.Add(false);
        allGifLists.Add(gif37);
        isEmo.Add(true);
        allGifLists.Add(gif38);
        isEmo.Add(false);
        allGifLists.Add(gif39);
        isEmo.Add(true);
        allGifLists.Add(gif40);
        isEmo.Add(false);
        allGifLists.Add(gif41);
        isEmo.Add(false);
        allGifLists.Add(gif42);
        isEmo.Add(true);
        allGifLists.Add(gif43);
        isEmo.Add(false);
        allGifLists.Add(gif44);
        isEmo.Add(false);
        allGifLists.Add(gif45);
        isEmo.Add(false);
        allGifLists.Add(gif46);
        isEmo.Add(true);
        allGifLists.Add(gif47);
        isEmo.Add(false);
        allGifLists.Add(gif48);
        isEmo.Add(false);
        allGifLists.Add(gif49);
        isEmo.Add(false);
        allGifLists.Add(gif50);
        isEmo.Add(false);
        allGifLists.Add(gif51);
        isEmo.Add(false);
        allGifLists.Add(gif52);
        isEmo.Add(false);
        allGifLists.Add(gif53);
        isEmo.Add(true);
        allGifLists.Add(gif54);
        isEmo.Add(true);
        allGifLists.Add(gif55);
        isEmo.Add(false);
        allGifLists.Add(gif56);
        isEmo.Add(true);
        allGifLists.Add(gif57);
        isEmo.Add(false);
        allGifLists.Add(gif58);
        isEmo.Add(false);
        allGifLists.Add(gif59);
        isEmo.Add(false);
        allGifLists.Add(gif70);
        isEmo.Add(false);
        allGifLists.Add(gif71);
        isEmo.Add(true);
        allGifLists.Add(gif72);
        isEmo.Add(false);
        allGifLists.Add(gif73);
        isEmo.Add(false);
        allGifLists.Add(gif74);
        isEmo.Add(false);
        allGifLists.Add(gif75);
        isEmo.Add(false);
        allGifLists.Add(gif76);
        isEmo.Add(false);
        allGifLists.Add(gif77);
        isEmo.Add(false);
        allGifLists.Add(gif78);
        isEmo.Add(false);
        allGifLists.Add(gif79);
        isEmo.Add(false);
        allGifLists.Add(gif80);
        //isEmo.Add(true);
        //allGifLists.Add(gifn1);
        isEmo.Add(false);
        allGifLists.Add(gifn2);
        isEmo.Add(true);
        allGifLists.Add(gifn3);
        isEmo.Add(false);
        allGifLists.Add(gifn4);
        isEmo.Add(false);
        allGifLists.Add(gifn5);
        isEmo.Add(false);
        allGifLists.Add(gifn6);
        isEmo.Add(false);
        allGifLists.Add(gifn7);
        isEmo.Add(false);
        allGifLists.Add(gifn8);
        isEmo.Add(false);
        allGifLists.Add(gifn9);
        isEmo.Add(false);
        allGifLists.Add(gifn10);
        isEmo.Add(false);
        allGifLists.Add(gifn11);
        isEmo.Add(false);
        allGifLists.Add(gifn12);
        isEmo.Add(true);
        allGifLists.Add(gifn13);
        isEmo.Add(false);
        allGifLists.Add(gifn14);
        isEmo.Add(true);
        allGifLists.Add(gifn15);
        isEmo.Add(false);
        allGifLists.Add(gifn16);
        isEmo.Add(false);
        allGifLists.Add(gifn17);
        isEmo.Add(false);
        allGifLists.Add(gifn18);
        isEmo.Add(false);
        allGifLists.Add(gifn19);
        isEmo.Add(false);
        allGifLists.Add(gifn20);
        isEmo.Add(false);
        allGifLists.Add(gifn21);
        isEmo.Add(false);
        allGifLists.Add(gifn22);
        isEmo.Add(false);
        allGifLists.Add(gifn23);
        isEmo.Add(false);
        allGifLists.Add(gifn24);
        isEmo.Add(false);
        allGifLists.Add(gifn25);
        isEmo.Add(true);
        allGifLists.Add(gifn26);
        isEmo.Add(false);
        allGifLists.Add(gifn27);
        isEmo.Add(false);
        allGifLists.Add(gifn28);
        isEmo.Add(false);
        allGifLists.Add(gifn29);
        isEmo.Add(false);
        allGifLists.Add(gifn30);
        isEmo.Add(false);
        allGifLists.Add(gifn31);
        isEmo.Add(false);
        allGifLists.Add(gifn32);
        isEmo.Add(false);
        allGifLists.Add(gifn33);
        isEmo.Add(false);
        allGifLists.Add(gifn34);
        isEmo.Add(false);
        allGifLists.Add(gifn35);
        isEmo.Add(false);
        allGifLists.Add(gifn36);
        isEmo.Add(false);
        allGifLists.Add(gifn37);
        isEmo.Add(false);
        allGifLists.Add(gifn38);
        isEmo.Add(false);
        allGifLists.Add(gifn39);
        isEmo.Add(false);
        allGifLists.Add(gifn40);
        isEmo.Add(false);
        allGifLists.Add(gifn41);
        isEmo.Add(false);
        allGifLists.Add(gifn42);
        isEmo.Add(false);
        allGifLists.Add(gifn43);
        isEmo.Add(false);
        allGifLists.Add(gifn44);
        isEmo.Add(false);
        allGifLists.Add(gifn45);
        isEmo.Add(false);
        allGifLists.Add(gifn46);
        isEmo.Add(false);
        allGifLists.Add(gifn47);
        isEmo.Add(true);
        allGifLists.Add(gifn48);
        isEmo.Add(false);
        allGifLists.Add(gifn49);
        isEmo.Add(true);
        allGifLists.Add(gifn50);
        isEmo.Add(true);
        allGifLists.Add(gifn51);
        isEmo.Add(true);
        allGifLists.Add(gifn52);
        isEmo.Add(true);
        allGifLists.Add(gifn53);
        isEmo.Add(true);
        allGifLists.Add(gifn54);
        isEmo.Add(true);
        allGifLists.Add(gifn55);
        isEmo.Add(true);
        allGifLists.Add(gifn56);
        isEmo.Add(true);

        // ... add all your gif lists

        // To create GIFs from each list, you can loop through all the lists in allGifLists
        int clipCount = 0; // Counter for unique animation names

        foreach (List<Sprite> gifList in allGifLists)
        {
           

            // Check if the animation already exists
                StartCoroutine(PlayAnimation(gifList, clipCount,isEmo[clipCount]));
            

            clipCount++;
        }
        // StartCoroutine(PlayAnimation(gif80, "sad"));
    }
   

    // Function to create an animation from a list of sprites


    // Coroutine to play an animation from a list of sprites
    IEnumerator PlayAnimation(List<Sprite> spriteList, int clipNum,bool parent)
    {
        GameObject animatedObject = Instantiate(prefab, transform.position, Quaternion.identity);
        animatedObject.transform.position=new Vector3(animatedObject.transform.position.x,animatedObject.transform.position.y,-2);
        Image spriteRenderer = animatedObject.GetComponent<Image>();
        animatedObject.SetActive(true);
        animatedObject.GetComponent<spriteSpics>().id=clipNum;
        if(parent){
        animatedObject.transform.SetParent(contentHolder2.transform,false);

        }
        else{
        animatedObject.transform.SetParent(contentHolder.transform,false);
            
        }
        //spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        
        
    while(true)
        foreach (Sprite sprite in spriteList)
        {
            
            spriteRenderer.sprite = sprite;
            //spriteRenderer.size=new Vector2(100.0f,100.0f);
            yield return new WaitForSeconds(frameTime);
        }
    }
     
}
