using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToogleImage : MonoBehaviour
{
 private Toggle toggle;
 public Sprite onSwitch, offSwitch;
 public GameObject settingsSaver;
public string VarName;
private void Awake()
{
  toggle = GetComponent<Toggle>();
}
 
 void Start()
 {
    toggle.onValueChanged.AddListener(onvalueChange);
 }
 private void onvalueChange(bool isOn)
 {
    if (toggle.isOn)
    {
        toggle.targetGraphic.GetComponent<Image>().sprite = onSwitch;
    }
    else
    {
                toggle.targetGraphic.GetComponent<Image>().sprite = offSwitch;   
    }
    settingsSaver.GetComponent<saveSettings>().save();
 }
void OnEnable()
{
    toggle.isOn=playerSettings.settings.getValue(VarName);
    if (toggle.isOn)
    {
        toggle.targetGraphic.GetComponent<Image>().sprite = onSwitch;
    }
    else
    {
                toggle.targetGraphic.GetComponent<Image>().sprite = offSwitch;   
    }
}
}
