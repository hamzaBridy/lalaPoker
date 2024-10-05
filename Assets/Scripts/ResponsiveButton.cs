using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResponsiveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    public float scaleFactor = 1.2f; // The factor by which the button will be scaled when pressed
    public float animationSpeed = 15f;
    private Vector3 originalScale;
    private bool isPressed;
    private bool isReleased;

    private void Awake()
    {
         button = GetComponent<Button>();
         originalScale = button.transform.localScale;

    }
    void Start()
    {
        if (!button)
        {
            button = GetComponent<Button>();
        }

        

        isPressed = false;
        isReleased = true;
    }

    private void OnEnable()
    {
        button.transform.localScale = originalScale;
    }
    void Update()
    {
        if (isPressed)
        {
            button.transform.localScale = Vector3.Lerp(button.transform.localScale, originalScale * scaleFactor, animationSpeed * Time.deltaTime);
        }

        if (isReleased && button.transform.localScale != originalScale)
        {
            button.transform.localScale = Vector3.Lerp(button.transform.localScale, originalScale, animationSpeed * Time.deltaTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button)
        {
            isPressed = true;
            isReleased = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button)
        {
            isPressed = false;
            isReleased = true;
        }
    }
}
