using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] CitizenSpawner citizenSpawner;
    [SerializeField] BuildingManager buildingManager;
    [SerializeField] CollectorManager resourceCollector;
    [SerializeField] CollectorManager foodCollector;
    [SerializeField] SlaveShooter slaveShooter;
    [SerializeField] CombatManager combatManager;
    [SerializeField] int intervall = 1;

    bool pause;

    public bool Pause
    {
        get
        {
            return pause;
        }
        set
        {
            pause = value;
            slaveShooter.pause = value;
            combatManager.pause = value;
        }
    }

    public void TogglePause()
    {
        Pause = !pause;
    }

    void Start()
    {
        StartCoroutine(ProductionFrame());
    }

    protected IEnumerator ProductionFrame()
    {
        yield return new WaitForSeconds(intervall);
        while (true)
        {
            if (pause)
            {
                yield return new WaitWhile(() => pause);
            }
            citizenSpawner.TrySpawn();
            resourceCollector.Collect();
            buildingManager.Build();
            foodCollector.Collect();
            yield return new WaitForSeconds(intervall);
        }
    }
}
