using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]Text text;
    [SerializeField] GameObject canvas;
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void WinLevel()
    {
        canvas.SetActive(true);
        text.text = "Congratulations, you build the Monument";
    }
}
