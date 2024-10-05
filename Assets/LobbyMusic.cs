using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMusic : MonoBehaviour
{
  private AudioSource lobbyMusic;

    private void Start()
    {
        playerSettings.OnSettingsChanged += UpdateSoundSettings;
        if(playerSettings.settings.sound)
        {
            lobbyMusic = GetComponent<AudioSource>();
            lobbyMusic.Play();
        }
    }
    private void OnDestroy()
    {
        playerSettings.OnSettingsChanged -= UpdateSoundSettings;

    }
    void UpdateSoundSettings(bool isOn)
    {
        if(isOn)
        {
            lobbyMusic = GetComponent<AudioSource>();
            lobbyMusic.Play();
        }
        else
        {
            lobbyMusic = GetComponent<AudioSource>();
            lobbyMusic.Stop();
        }
    }
}
