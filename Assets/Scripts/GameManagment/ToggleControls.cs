using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControls : MonoBehaviour
{
    [SerializeField] Toggle toggle1;
    [SerializeField] Toggle toggle10;
    [SerializeField] Toggle toggle100;
	[SerializeField] Toggle toggle1000;
    [SerializeField] Toggle toggleExtraFood;
    [SerializeField] LevelTimer levelTimer;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            toggle1.isOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            toggle10.isOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            toggle100.isOn = true;
        }
		else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            toggle1000.isOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            toggleExtraFood.isOn = !toggleExtraFood.isOn;
        }
        else if (Input.GetButtonDown("Pause"))
        {
            levelTimer.TogglePause();
        }
    }
}
