using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAlphaController : MonoBehaviour
{
    public Button[] buttons;
    private Image[] buttonBackgrounds;
    private Coroutine fadeRoutine;

    [SerializeField]
    private float fadeDuration = 0.3f;

    private void Start()
    {
        buttonBackgrounds = new Image[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttonBackgrounds[i] = buttons[i].GetComponent<Image>();
            buttons[i].onClick.AddListener(() => SetActiveButton(index));
        }

        InitializeAlphaValues(0);
    }

    private void Awake()
    {
        InitializeButtonBackgrounds();
    }

    private void OnEnable()
    {
        InitializeAlphaValues(0);
    }
    private void InitializeButtonBackgrounds()
    {
        if (buttonBackgrounds == null) // Check if already initialized
        {
            buttonBackgrounds = new Image[buttons.Length];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttonBackgrounds[i] = buttons[i].GetComponent<Image>();
                // Add other initialization code if needed
            }
        }
    }
    private void InitializeAlphaValues(int activeIndex)
    {
        for (int i = 0; i < buttonBackgrounds.Length; i++)
        {
            SetAlpha(buttonBackgrounds[i], i == activeIndex ? 1f : 0f);
        }
    }
   

    private void SetActiveButton(int activeIndex)
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        fadeRoutine = StartCoroutine(FadeToButton(activeIndex));
    }

    private IEnumerator FadeToButton(int activeIndex)
    {
        for (int i = 0; i < buttonBackgrounds.Length; i++)
        {
            if (i == activeIndex)
            {
                StartCoroutine(FadeAlpha(buttonBackgrounds[i], 1f, fadeDuration));
            }
            else
            {
                StartCoroutine(FadeAlpha(buttonBackgrounds[i], 0f, fadeDuration));
            }
        }
        yield return null;
    }

    private IEnumerator FadeAlpha(Image img, float targetAlpha, float duration)
    {
        float startAlpha = img.color.a;
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            SetAlpha(img, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        SetAlpha(img, targetAlpha); 
    }

    private void SetAlpha(Image img, float alpha)
    {
        Color color = img.color;
        color.a = alpha;
        img.color = color;
    }
}
