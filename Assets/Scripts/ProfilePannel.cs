using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static ProfileInfo;
using static Unity.Collections.AllocatorManager;

public class ProfilePannel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI balanceC, handsPlayedC, biggestWinC, levelC, level1C, xpPointsC, vipLevelC, ljpChipsWonC, ljpWinsC, userNameC, winPerentageC;
    public Texture DefalutInamge;
    [SerializeField] private RawImage profileImage;
    [SerializeField] private Image winPercentImage;
    [SerializeField] private Button addFriendButton, blockButton, reportButton, sendChipsButton;
    [SerializeField] private TextMeshProUGUI addFriendText, blockText, reportText;
    [SerializeField] private GameObject editButton,notificationPannel;
    // public GameObject sendChipsContainer;
    private bool isFriend = false;
    public bool isBlocked = false, FriendRequestSent = false;
    public string balance, handsPlayed, biggestWin, level, xpPoints, vipLevel, ljpChipsWon, ljpWins, imageUrl, userName, id, friendShipId,winPerentage;
    public APIcalls aPIcalls;
    public Sprite defaultSprite;
    private void Awake()
    {
        APIcalls.OnGetStatues += UpdateStatues;
        APIcalls.OnUserBlocked += Blocked;
        APIcalls.OnUserUnBlocked += Unblocked;

        AvatarsImageScript.OnPhotoChanged += updatePhoto;
    }
    private void OnEnable()
    {
        if(notificationPannel != null)
        notificationPannel.SetActive(false);
        if(editButton != null)
        {
        if(id == ProfileInfo.Player.id)
        { editButton.SetActive(true);
        }
        else
        {
            editButton.SetActive(false);
        }
        }
       
    }
    private void Start()
    {
        if(blockButton != null)
            blockButton.onClick.AddListener(BlockOrUnblock);

    }
    private void OnDestroy()
    {
        APIcalls.OnGetStatues -= UpdateStatues;
        AvatarsImageScript.OnPhotoChanged -= updatePhoto;
        APIcalls.OnUserBlocked -= Blocked;
        APIcalls.OnUserUnBlocked -= Unblocked;

    }
    private void Blocked()
    {
        if(blockButton != null)
        blockText.text = "Unblock";

    }
    private void Unblocked()
    {
        if (blockButton != null)
        blockText.text = "block";

    }
    private void UpdateStatues()
    {
        profileImage.texture=DefalutInamge;


        if (friendShipId == "")
        {

            isFriend = false;

        }
        else
        {

            isFriend = true;
        }

        updateButtons();
        winPercentImage.fillAmount = float.Parse(winPerentage) / 100;
        balanceC.text = "$" + balance;
        handsPlayedC.text = handsPlayed;
        

        biggestWinC.text = "$" + biggestWin;
        levelC.text = level;
        level1C.text = level;
        xpPointsC.text = xpPoints;
        vipLevelC.text = vipLevel;
        ljpChipsWonC.text = "$" +  ljpChipsWon;
        ljpWinsC.text = ljpWins;
        userNameC.text = userName;
        //Debug.Log(userName);
        //userNameC.text="qwer";
        winPerentageC.text = winPerentage + "%";
        StartCoroutine(LoadPhoto(profileImage, imageUrl));
    }
    void updatePhoto(Sprite updatedSprite)
    {
        profileImage.texture = updatedSprite.texture;
    }
    public void Add_or_RemoveFriend()
    {
        if (isFriend)
        {
            Delete_Friend();
        }
        else
        {
            Send_FriendRequest();
        }
    }
    private void updateButtons()
    {
        if (isFriend && sendChipsButton != null)
        {
            sendChipsButton.gameObject.SetActive(true);
        }
       
       
        if (id == ProfileInfo.Player.id && sendChipsButton != null)
        {
            sendChipsButton.gameObject.SetActive(false);

        }
        if (isFriend && addFriendButton != null)
        {
            addFriendText.text = "Remove Friend";
            sendChipsButton.gameObject.SetActive(true);
            VertexGradient gradient = new VertexGradient(Color.white, Color.red, Color.red, Color.white);
            addFriendText.enableVertexGradient = true;
            addFriendText.colorGradient = gradient;
            addFriendText.ForceMeshUpdate(); 
        }
        
        if (!isFriend && addFriendButton != null)
        {
            if(FriendRequestSent)
            {
                addFriendText.text = "Request sent";
                sendChipsButton.gameObject.SetActive(false);
                VertexGradient gradient = new VertexGradient(Color.white, Color.green, Color.green, Color.white);
                addFriendText.enableVertexGradient = true;
                addFriendText.colorGradient = gradient;
                addFriendText.ForceMeshUpdate();
            }
            else
            {
                addFriendText.text = "Add Friend";
                sendChipsButton.gameObject.SetActive(false);
                VertexGradient gradient = new VertexGradient(Color.white, Color.green, Color.green, Color.white);
                addFriendText.enableVertexGradient = true;
                addFriendText.colorGradient = gradient;
                addFriendText.ForceMeshUpdate();
            }
             
        }
        
        if (id == ProfileInfo.Player.id && addFriendButton != null)
        {
            addFriendButton.gameObject.SetActive(false);
            sendChipsButton.gameObject.SetActive(false);
          //  blockButton.gameObject.SetActive(false);
           // reportButton.gameObject.SetActive(false);

        }
        if (id != ProfileInfo.Player.id && addFriendButton != null)
        {

            addFriendButton.gameObject.SetActive(true);
            if(isFriend)
            sendChipsButton.gameObject.SetActive(true);

           // blockButton.gameObject.SetActive(true);
            //reportButton.gameObject.SetActive(true);

        }
        if (id == ProfileInfo.Player.id && reportButton != null)
        {
           
             blockButton.gameObject.SetActive(false);
             reportButton.gameObject.SetActive(false);

        }
        if (id != ProfileInfo.Player.id && reportButton != null)
        {

           

            blockButton.gameObject.SetActive(true);
            reportButton.gameObject.SetActive(true);

        }
        // if(id == ProfileInfo.Player.id){
        // sendChipsButton.SetActive(false);
        // }
        // else{
        // sendChipsButton.SetActive(true);

        // }
        if (isBlocked && blockButton != null)
        {
            blockText.text = "Unblock";
        }
        if (!isBlocked && blockButton != null)
        {
            blockText.text = "Block";
        }
    }
    public void BlockOrUnblock()
    {
        if (blockText.text == "Unblock")
        {
            aPIcalls.UnBlockUserWrapper(id);
        }
        else if (blockText.text =="Block")
        {
            aPIcalls.BlockUserWrapper(id);

        }
    }
    public void Delete_Friend()
    {
        aPIcalls.deleteFriendRequestWrapper(friendShipId);


    }
    public void Send_FriendRequest()
    {
        aPIcalls.SendFriendRequestWrapper(id);
        addFriendText.text = "Request sent";
    }
    private IEnumerator LoadPhoto(RawImage profileImage, string imageUrl)
    {
        if (imageUrl != "")
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D photoTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Sprite photoSprite = Sprite.Create(photoTexture, new Rect(0, 0, photoTexture.width, photoTexture.height), new Vector2(0.5f, 0.5f));

                profileImage.texture = photoSprite.texture;

            }
        }
    }
}
