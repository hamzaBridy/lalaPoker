using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class sliderValue : MonoBehaviour
{
    public List<Sprite> sprites;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int spriteChoes = (int)(getSliderValue.TableSliderValue * (float)(sprites.Count - 1) / getSliderValue.maxSliderValue);
        GetComponent<Image>().sprite = sprites[spriteChoes];
    }
}
