using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] GameObject canvas;
    [SerializeField] LevelTimer levelTimer;
    AudioSource audioSource;
    [SerializeField] AudioClip winMusic;

    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GameObject.Find("MainCamera").GetComponent<AudioSource>();
    }

    public void WinLevel()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(winMusic);
        levelTimer.Pause = true;
        canvas.SetActive(true);
        text.text = "Congratulations, you build the Monument";
    }
}
