using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class userlevelUpPrefabScript : MonoBehaviour
{
    public float moveDistance = 15f;
    public float moveSpeed = 10f;
    public float fadeSpeed = 0.3f;
    public Image spriteRenderer;
    public TextMeshProUGUI level;
    public string newLevel;
    private void Start()
    {
        gameObject.transform.localScale = Vector3.one;
        StartCoroutine(MoveAndFade());
    }
    public void setParam(string newLevel)
    {
        level.text = newLevel;
        Debug.Log(newLevel);
        Debug.Log(level.text);
    }
        IEnumerator MoveAndFade()
    {
        // Calculate the target position to move to.
        Vector3 targetPosition = transform.position + new Vector3(0, moveDistance, 0);

        // Move upwards to the target position.
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Fade out the sprite.
        while (spriteRenderer.color.a > 0.01f)
        {
            Color currentColor = spriteRenderer.color;
            currentColor.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = currentColor;
            yield return null;
        }

        // Destroy the game object.
        Destroy(gameObject);
    }
}