using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] GameObject canvas;
    LevelTimer levelTimer;
    AudioSource audioSource;
    [SerializeField] AudioClip winMusic;

    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GameObject.Find("MainCamera").GetComponent<AudioSource>();
        levelTimer = FindObjectOfType<LevelTimer>();
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
