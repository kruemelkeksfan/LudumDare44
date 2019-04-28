using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] BuildingManager buildingManager;
    [SerializeField] GameObject spawnPointHolder;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] int attackChance;
    [SerializeField] int minEnemyCount;
    [SerializeField] int maxEnemyCount;
    [SerializeField] int waitForAttackTime;
    [SerializeField] int progressLoss;
    [SerializeField] float spawnIntervall;
    public List<Enemy> enemies;

    Transform[] spawnPointChilds;
    List<Transform> enemySpawnPoints;
    bool combat;

    void Start()
    {
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
        if (combat && enemies.Count < 1)
        {
            //endMusic
            combat = false;
        }
    }

    public void RollForAttack()
    {
        int roll = Random.Range(0, 100);
        if (roll < attackChance)
        {
            //startFightMusik
            StartCoroutine(WaitForAttack());
        }
    }

    void StartAttack()
    {
        combat = true;
        int roll = Random.Range(minEnemyCount, maxEnemyCount + 1);
        StartCoroutine(Spawn(roll));
    }

    IEnumerator Spawn(int roll)
    {
        int count = 0;
        while (count < roll)
        {
            int rollSpawn = Random.Range(0, enemySpawnPoints.Count);
            Instantiate(enemyPrefab, enemySpawnPoints[rollSpawn]);
            count++;
            yield return new WaitForSeconds(spawnIntervall);
        }
    }

    public void Hit (Enemy enemy)
    {
        //play sfx
        buildingManager.LossProgress(progressLoss);
    }

    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(waitForAttackTime);
        StartAttack();
    }
}
