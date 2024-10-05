using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LuckyWheelScript : MonoBehaviour
{
    [SerializeField] private Transform movingWheelPart;
    [SerializeField] private APIcalls aPIcallsRef;
   // [SerializeField] TextMeshProUGUI amountHolder;
    [SerializeField] GameObject winanimation, BlackOverlay, luckyWheelMenu;
    public AudioSource audiosource;

    private void Start()
    {
        aPIcallsRef.OnLuckyWheelSpin += spinWheel;
    }
    private void OnDestroy()
    {
        aPIcallsRef.OnLuckyWheelSpin -= spinWheel;

    }
    private void spinWheel(long chips)
    {
        float targetAngle = GetTargetAngle(chips);
        StartCoroutine(SpinWheelangle(targetAngle, chips));
       // Debug.Log("start");
    }
    float GetTargetAngle(long prize)
    {
        float angle = 0;

        switch (prize)
        {
            case 2000000000:
                angle = 0;
                break;
            case 500000000:
                angle = 30;
                break;
            case 600000000:
                angle = 60;
                break;
            case 700000000:
                angle = 90;
                break;
            case 800000000:
                angle = 120;
                break;
            case 900000000:
                angle = 150;
                break;
            case 1000000000:
                angle = 180;
                break;
            case 1010000000:
                angle = 210;
                break;
            case 1200000000:
                angle = 240;
                break;
            case 1300000000:
                angle = 270;
                break;
            case 1400000000:
                angle = 300;
                break;
            case 1500000000:
                angle = 330;
                break;

        }
        return angle;
    }
    IEnumerator SpinWheelangle(float targetAngle, long chips)
    {
        Debug.Log("SpinWheelangle");

        float currentAngle = movingWheelPart.eulerAngles.z;
        float rotationSpeed = 250f; // Set to a high value for fast initial spin
        float minimumSpeed = 170f; // The slowest speed the wheel will rotate
        float slowDownRate = 0.75f; // Rate at which the wheel will slow down

        int spinRounds = 2; // Number of initial fast spin rounds
        float totalRotation = spinRounds * 360 + targetAngle; // Total rotation angle
        if (playerSettings.settings.sound)
            audiosource.Play();
        // Ensure the wheel spins until it reaches the target angle
        while (currentAngle < totalRotation)
        {
            currentAngle += rotationSpeed * Time.deltaTime;
            movingWheelPart.eulerAngles = new Vector3(0, 0, currentAngle % 360); // Keep angle in [0, 360)

            // Start slowing down after initial rounds are over
            if (currentAngle >= (spinRounds - 1) * 360 && rotationSpeed > minimumSpeed)
                rotationSpeed -= slowDownRate;



            yield return null;
        }

        movingWheelPart.eulerAngles = new Vector3(0, 0, targetAngle);
        if (playerSettings.settings.sound)
            audiosource.Stop();
        yield return new WaitForSeconds(0.5f);
          //  amountHolder.text = "COLLECT" + " " + MoneyConverter.ConvertMoney(chips);
            winanimation.SetActive(true);
            BlackOverlay.SetActive(true);           
            aPIcallsRef.GetLuckyWheelTimeWrapper();
            yield return new WaitForSeconds(5f);
            winanimation.SetActive(false);
            BlackOverlay.SetActive(false);
            this.GetComponent<PanelFadeOut>().OnDisable();
            aPIcallsRef.getStatuesWrapper(ProfileInfo.MyPlayer.user_id,false);

    }
}
