using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Transform))]
public class PopEffect : MonoBehaviour
{
    public float popDuration = 0.2f; // Duration of the pop effect
    private Vector3 originalScale;    // Store the original scale of the object
    public Scrollbar profileSlider, chatSlidere;
    public Slider slider;
    private void Awake()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero; // Set initial scale to 0
    }

    private void OnEnable()
    {
        StartCoroutine(Pop());
        
    }

    private IEnumerator Pop()
    {
        float elapsedTime = 0f;

        while (elapsedTime < popDuration)
        {
            float t = elapsedTime / popDuration;
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
            if (profileSlider != null)
                profileSlider.value = 1f;
            if (slider != null)
                slider.value = 1f;
            if (chatSlidere != null)
                chatSlidere.value = 1f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale; // Ensure it's set to the original scale at the end

    }
}
