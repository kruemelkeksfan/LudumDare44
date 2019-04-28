using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountToggleControlls : MonoBehaviour
{
    [SerializeField] Toggle toggle1;
    [SerializeField] Toggle toggle10;
    [SerializeField] Toggle toggle100;

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
    }
}
