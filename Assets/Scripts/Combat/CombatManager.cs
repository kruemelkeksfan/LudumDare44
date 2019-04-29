using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] GameObject attackWarning;
    [SerializeField] BuildingManager buildingManager;
    [SerializeField] GameObject spawnPointHolder;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] int attackChance;
    [SerializeField] int minEnemyCount;
    [SerializeField] int maxEnemyCount;
    [SerializeField] int waitForAttackTime;
    [SerializeField] int progressLoss;
    [SerializeField] float spawnIntervall;
    [SerializeField] AudioClip fightMusic;
    [SerializeField] AudioClip normalMusic;
    [SerializeField] AudioSource hitSfx;
    [SerializeField] float fadeSpeed;
    public List<Enemy> enemies;

    Transform[] spawnPointChilds;
    List<Transform> enemySpawnPoints;
    bool combat;
    bool waiting;
    bool fadeOut = false;
    bool fadeIn = false;
    public bool pause;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.Find("MainCamera").GetComponent<AudioSource>();
        enemySpawnPoints = new List<Transform>();
        spawnPointChilds = spawnPointHolder.GetComponentsInChildren<Transform>();
        foreach (Transform spawnPointChild in spawnPointChilds)
        {
            if (spawnPointChild.parent == spawnPointHolder.transform)
            {
                enemySpawnPoints.Add(spawnPointChild);
            }
        }
    }

    void Update()
    {
        if (combat && !waiting && enemies.Count < 1)
        {
            attackWarning.SetActive(false);          
            combat = false;
            fadeOut = true;
        }

        if (fadeOut == true)
        {
            audioSource.volume -= fadeSpeed * Time.deltaTime;
            if (audioSource.volume <= 0)
            {
                if(combat == true)
                {
                    audioSource.clip = fightMusic;
                }

                else
                {
                    audioSource.clip = normalMusic;
                }
                
                audioSource.Play();
                fadeOut = false;
                fadeIn = true;
            }
        }

        if (fadeIn == true)
        {
            audioSource.volume += fadeSpeed * Time.deltaTime;
            if (audioSource.volume >= 0.4f)
            {
                fadeIn = false;
            }
        }
    }

    public void RollForAttack()
    {
        int roll = Random.Range(0, 100);
        if (roll < attackChance)
        {
            StartCoroutine(WaitForAttack());
            if (combat == false)
            {
                combat = true;
                waiting = true;
                fadeOut = true;
                attackWarning.SetActive(true);                          
            }
        }
    }

    void StartAttack()
    {        
        int roll = Random.Range(minEnemyCount, maxEnemyCount + 1);
        StartCoroutine(Spawn(roll));
        waiting = false;
    }

    IEnumerator Spawn(int roll)
    {
        int count = 0;
        while (count < roll)
        {
            int rollSpawn = Random.Range(0, enemySpawnPoints.Count);
            Enemy newEnemy = Instantiate(enemyPrefab, enemySpawnPoints[rollSpawn]);
            enemies.Add(newEnemy);
            count++;
            yield return new WaitForSeconds(spawnIntervall);
        }
    }

    public void Hit (Enemy enemy)
    {
        hitSfx.Play();
        buildingManager.LossProgress(progressLoss);
    }

    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(waitForAttackTime);
        if (pause)
        {
            yield return new WaitWhile(() => pause);
        }
        StartAttack();
    }
}
