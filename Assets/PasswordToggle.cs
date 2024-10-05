using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordToggle : MonoBehaviour
{
    public TMP_InputField passwordInputField;
    private Toggle showHideToggle;
    [SerializeField] private Sprite hide, unHide;
    [SerializeField] private Image toggleImage;

    private void Start()
    {
        showHideToggle = GetComponent<Toggle>();
        // Subscribe to the onValueChanged event of the toggle
        showHideToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        // Change the input field content type based on the toggle state
        passwordInputField.contentType = isOn ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        toggleImage.sprite = isOn ? hide : unHide;

        // Update the text to refresh the display
        passwordInputField.ForceLabelUpdate();
    }
}
