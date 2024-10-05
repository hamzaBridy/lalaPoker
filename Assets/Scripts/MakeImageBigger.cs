using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MakeImageBigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float scaleFactor = 3f;
    public float scaleDuration = 0.2f;
    public Vector3 moveAmount = new Vector3(55f, 0, 0);

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Coroutine scaleCoroutine;
    private int originalSiblingIndex;

    private Canvas canvas;

    private void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
        canvas = GetComponentInParent<Canvas>();
        originalSiblingIndex = transform.GetSiblingIndex();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();

        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        // Set the sorting order to a high value to ensure it's on top
        if (canvas != null)
        {
            canvas.sortingOrder = 1000;
        }

        Vector3 targetPosition = originalPosition + moveAmount;
        scaleCoroutine = StartCoroutine(ScaleAndMoveOverTime(scaleFactor, targetPosition, scaleDuration));
    }


    public void OnPointerUp(PointerEventData eventData)
    {

        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        // Reset the sorting order
        if (canvas != null)
        {
            canvas.sortingOrder = 0;
        }

        scaleCoroutine = StartCoroutine(ScaleAndMoveOverTime(1, originalPosition, scaleDuration));
        transform.SetSiblingIndex(originalSiblingIndex);

    }


    private IEnumerator ScaleAndMoveOverTime(float targetScaleFactor, Vector3 targetPosition, float duration)
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(originalScale.x * targetScaleFactor, originalScale.y * targetScaleFactor, originalScale.z);

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = targetPosition;

        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, (Time.time - startTime) / duration);
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, (Time.time - startTime) / duration);
            yield return null;
        }

        transform.localScale = endScale;
        transform.localPosition = endPosition;
    }
}