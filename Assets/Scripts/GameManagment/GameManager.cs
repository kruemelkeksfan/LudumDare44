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
    CombatManager combatManager;
    [SerializeField] AudioClip winMusic;

    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GameObject.Find("MainCamera").GetComponent<AudioSource>();
        combatManager = FindObjectOfType<CombatManager>();
        levelTimer = FindObjectOfType<LevelTimer>();
    }

    public void WinLevel()
    {
        if (combatManager.combat)
        {
            StartCoroutine(CheckForCombat());
        }
        else
        {
            audioSource.Stop();
            audioSource.PlayOneShot(winMusic);
            levelTimer.Pause = true;
            canvas.SetActive(true);
            text.text = "Congratulations, you build the Monument";
        }
    }

    IEnumerator CheckForCombat()
    {
        yield return new WaitWhile(() => combatManager.combat);
        WinLevel();
    }
}
