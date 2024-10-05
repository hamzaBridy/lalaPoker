using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InviteTogamePannel : MonoBehaviour
{
    [SerializeField] private Slider timeSlider;
    [SerializeField] private float time = 10f;
    public TextMeshProUGUI inviteText;
    [SerializeField] private Button JoinButton;
    [SerializeField] private GameObject invitePannel;
    [SerializeField] private webSocetConnect webSocetConnectRef;
    private void Start()
    {
        LobbyEvents.OnPlayerInvitedToLobby += GotInvited;
    }
    private void OnDestroy()
    {
        LobbyEvents.OnPlayerInvitedToLobby -= GotInvited;

    }
    private IEnumerator IncreaseSliderOverTime(float duration)
    {
        float timer = 0;

        while (timer <= duration)
        {
            timeSlider.value = timer;
            timer += Time.deltaTime;
            yield return null;
        }

        // Set to max in case time exceeds duration due to deltaTime inaccuracy
        timeSlider.value = timeSlider.maxValue;
        invitePannel.SetActive(false);
    }
    
    private void GotInvited(LobbyEventArgs args)
    {

        invitePannel.SetActive(true);

        inviteText.text = args.LobbyEvent.inviterUserName  + " has invited you to a game";
        JoinButton.onClick.AddListener(()=> webSocetConnectRef.joinGameWrapper(args.LobbyEvent.InviterGameId));
        timeSlider.minValue = 0;
        timeSlider.maxValue = time;
        timeSlider.value = timeSlider.minValue;

        StartCoroutine(IncreaseSliderOverTime(time));
    }
   
}
