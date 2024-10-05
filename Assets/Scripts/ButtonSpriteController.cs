using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteController : MonoBehaviour
{
    public Button[] buttons;
    public Sprite pressedSprite;
    public Sprite unpressedSprite;
    private Image[] buttonBackgrounds;
    public Transform childernContainer;

    private void Start()
    {
        buttonBackgrounds = new Image[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; 
            buttonBackgrounds[i] = buttons[i].GetComponent<Image>();
            buttons[i].onClick.AddListener(() => SetActiveButton(index));
        }

        SetActiveButton(0);
        
    }

    private void Awake()
    {
        InitializeButtonBackgrounds();
    }
    private void InitializeButtonBackgrounds()
    {
        if (buttonBackgrounds == null) // Check if already initialized
        {
            buttonBackgrounds = new Image[buttons.Length];

            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i;
                buttonBackgrounds[i] = buttons[i].GetComponent<Image>();
                buttons[i].onClick.AddListener(() => SetActiveButton(index));
            }
        }
    }
    private void OnEnable()
    {
        SetActiveButton(0);
    }

    private void SetActiveButton(int activeIndex)
    {
        for (int i = 0; i < buttonBackgrounds.Length; i++)
        {
            if (i == activeIndex)
            {
                buttonBackgrounds[i].sprite = pressedSprite;
            }
            else
            {
                buttonBackgrounds[i].sprite = unpressedSprite;
            }
        }
    }
    
}
