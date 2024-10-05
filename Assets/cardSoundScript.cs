using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardSoundScript : MonoBehaviour
{
    private AudioSource cardAudio;

    private void OnEnable()
    {
        if(playerSettings.settings.sound)
        {
            cardAudio = GetComponent<AudioSource>();
            cardAudio.Play();
        }
       StartCoroutine(DestroySelf());
    }
    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3f);
        if(gameObject != null) Destroy(gameObject);
    }
}
