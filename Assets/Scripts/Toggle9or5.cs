using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle9or5 : MonoBehaviour
{
    public Toggle toggle9Players, toggle5Players;
    void Start()
    {
        toggle5Players.onValueChanged.AddListener(OnToggle5PlayersValueChanged);
        toggle9Players.onValueChanged.AddListener(OnToggle9PlayersValueChanged);
        OnToggle5PlayersValueChanged(toggle5Players.isOn);
        OnToggle9PlayersValueChanged(toggle9Players.isOn);

    }
    private void OnToggle5PlayersValueChanged(bool isOn)
    {
        if (isOn)
        {
            // 5 players selected

            if (toggle9Players.isOn)
            {
                toggle9Players.isOn = false;
            }
        }
        else
        {
            toggle9Players.isOn = true;

            // No toggle selected
            GameVariables.gameSize = "9";
        }
    }

    private void OnToggle9PlayersValueChanged(bool isOn)
    {
        if (isOn)
        {
            // 9 players selected
            GameVariables.gameSize = "9";

            // Uncheck the other toggle
            if (toggle5Players.isOn)
            {
                toggle5Players.isOn = false;
            }
        }
        else
        {
            toggle5Players.isOn = true;

            // No toggle selected
            GameVariables.gameSize = "5";
        }
    }
}
