using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class winText : MonoBehaviour
{
    JplayerData parentPlayer;
    public GameObject goldWinningAnimation;
    public GameObject potPrefab,jpPrefab,ljPrefab;
    private GameObject mainPot,jPot,LjPot;
    [SerializeField] private GameObject  winningTextPrefab;
    private GameObject winningTextTransform, currentWinningText;
    void Start()
    {
        webSocetConnect.UIWebSocket.MainTable.OnWin += setWinText;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart += newRountSetText;
        PlayersManager.OnSingleUser += newRountSetText;
        webSocetConnect.UIWebSocket.OnWinFOAK += WonJp;
        webSocetConnect.UIWebSocket.OnWinRoyalFush += WonRoyalFlush;

        winningTextTransform = GameObject.FindGameObjectWithTag("winningText");
        mainPot = GameObject.FindGameObjectWithTag("mainPot");
        jPot = GameObject.FindGameObjectWithTag("jPot");
        LjPot = GameObject.FindGameObjectWithTag("LjPot");

        parentPlayer = transform.parent.GetComponent<PlayerGameId>().player;
       
        
    }
    void WonJp(int userId, long chipsWon)
    {
        if(parentPlayer.userId == userId.ToString())
        {
            Debug.Log(userId);
            
                StartCoroutine(MoveJPToWinner(jpPrefab, transform, chipsWon,jPot));
            
        }

    }
    void WonRoyalFlush(int userId, long chipsWon)
    {
        if (parentPlayer.userId == userId.ToString())
        {
            Debug.Log(userId);

            StartCoroutine(MoveJPToWinner(ljPrefab, transform, chipsWon, LjPot));

        }

    }

    void setWinText()
    {
        if (parentPlayer.HasWon)
        {
            foreach (var item in parentPlayer.potPrio)
            {
                StartCoroutine(MovePotToWinner(potPrefab, this.transform, GameVariables.maxpotProi-item.Key,item.Key));
            }

           // StartCoroutine(MovePotToWinner(potPrefab, this.transform, parentPlayer.potPrio - 1));
        }
        else
        {
            goldWinningAnimation.SetActive(false);
        }
    }
    IEnumerator MoveJPToWinner(GameObject movingPot, Transform winnerPosition, long amountWon,GameObject startTransform)
    {
        GameObject newPot = Instantiate(movingPot, winnerPosition);
        float duration = 1.0f;
        float elapsedTime = 0;
        Vector3 startPosition = startTransform.transform.position;
        TextMeshProUGUI winText = newPot.GetComponentInChildren<TextMeshProUGUI>();
           winText.text = MoneyConverter.ConvertMoney(amountWon);

        while (elapsedTime < duration)
        {
            newPot.transform.position = Vector3.Lerp(startPosition, winnerPosition.position, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        newPot.transform.position = winnerPosition.position;
        StartCoroutine(GrowShrinkDestroyRoutine(newPot));
    }
    IEnumerator MovePotToWinner(GameObject movingPot, Transform winnerPosition, float delayTime,int prio)
    {
        yield return new WaitForSeconds(2 * delayTime);
        DisplayWinningText(); 
        GameObject newPot = Instantiate(movingPot, winnerPosition);
        float duration = 1.0f; 
        float elapsedTime = 0;
        Vector3 startPosition = mainPot.transform.position;
        TextMeshProUGUI winText = newPot.GetComponentInChildren<TextMeshProUGUI>();
        if (parentPlayer.Wining.ContainsKey(prio))
        winText.text = MoneyConverter.ConvertMoney(long.Parse(parentPlayer.Wining[prio]));
        while (elapsedTime < duration)
        {
            newPot.transform.position = Vector3.Lerp(startPosition, winnerPosition.position, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        newPot.transform.position = winnerPosition.position;
        StartCoroutine(GrowShrinkDestroyRoutine(newPot));
    }
    void DisplayWinningText()
    {
        
        if (currentWinningText != null)
        {
            Destroy(currentWinningText);
        }
        if (parentPlayer.WiningCompo != null)
        {
            currentWinningText = Instantiate(winningTextPrefab, winningTextTransform.transform);
        
            currentWinningText.GetComponentInChildren<TextMeshProUGUI>().text = parentPlayer.WiningCompo;
        }
            goldWinningAnimation.SetActive(true);
    }

    void newRountSetText()
    {
        if(currentWinningText != null) {
            Destroy(currentWinningText);
        }
        goldWinningAnimation.SetActive(false);
    }

    public void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnWin -= setWinText;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart -= newRountSetText;
        PlayersManager.OnSingleUser-=newRountSetText;
        webSocetConnect.UIWebSocket.OnWinFOAK -= WonJp;
        webSocetConnect.UIWebSocket.OnWinRoyalFush -= WonRoyalFlush;

    }
    private IEnumerator GrowShrinkDestroyRoutine(GameObject gameObject)
    {
        yield return new WaitForSeconds(1.5f);
        float growFactor = 1.1f; // How much the object will grow
        float shrinkSpeed = 0.8f; // How fast the object will shrink
        float minSize = 0.01f; // Minimum size before the object gets destroyed

        // Grow the object
        gameObject.transform.localScale *= growFactor;

        // Wait for a frame so the growth is visible
        yield return null;

        // Shrink the object until it's smaller than the minimum size
        while (gameObject.transform.localScale.x > minSize && gameObject.transform.localScale.y > minSize)
        {
            gameObject.transform.localScale *= shrinkSpeed;
            yield return null;
        }
        if(gameObject != null) {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(1.5f);
        goldWinningAnimation.SetActive(false);
    }
}
