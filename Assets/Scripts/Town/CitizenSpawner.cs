using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CitizenSpawner : MonoBehaviour
{
    [SerializeField] int spawnChance = 60;
    [SerializeField] int citizenDisplayedPerCount = 10;
    [SerializeField] int trys = 4;
    [SerializeField] FoodManager foodManager;
    [SerializeField] Toggle food;
    [SerializeField] CitizenGroup[] citizenGroupPrefabs;
    [SerializeField] GameObject spawnPointHolder;
    [SerializeField] SlaveManager slaveManager;

    SpawnPoint[] spawnPoints;

    void Start()
    {
        spawnPoints = spawnPointHolder.GetComponentsInChildren<SpawnPoint>();
    }

    public void TrySpawn()
    {
        if (!food.isOn)
        {
            return;
        }
        int count = 0;
        int currentTrys = Mathf.RoundToInt(trys * 0.5f);
        if (currentTrys < 1)
        {
            currentTrys = 1;
        }
        while (count < currentTrys)
        {
            int roll = Random.Range(0, 100);
            if (roll < spawnChance)
            {
                roll = Random.Range(0, spawnPoints.Length);
                if (spawnPoints[roll].CitizenGroup == null)
                {
                    int rollGroup = Random.Range(0, citizenGroupPrefabs.Length);
                    int displayedCitizen = 1;
                    if (rollGroup > citizenDisplayedPerCount)
                    {
                        displayedCitizen = Mathf.RoundToInt(rollGroup * 0.1f);
                    }

                    CitizenGroup newCitizenGroup = Instantiate(citizenGroupPrefabs[rollGroup], spawnPoints[roll].transform);
                    newCitizenGroup.SlaveManager = slaveManager;
                    int citizenCount = newCitizenGroup.ClaculateCitizenCount(0.5f);
                    foodManager.AddFoodNeededForSpawning(citizenCount);
                    newCitizenGroup.transform.localPosition = Vector3.zero;
                    newCitizenGroup.spawnPoint = spawnPoints[roll];
                    spawnPoints[roll].CitizenGroup = newCitizenGroup;
                }
            }
            count++;
        }
    }
}
