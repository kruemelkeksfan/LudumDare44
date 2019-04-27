using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] Text SlaveCountDisplay;
    [SerializeField] string preText = "free slave Count: ";
    [SerializeField] Transform building;
    [SerializeField] int intervall;
    [SerializeField] int gesProgress;
    [SerializeField] int progressPerWorker;
    [SerializeField] int materialPerWorker;
    [Header("Dying Worker")]
    [SerializeField] int highDeathCount = 100;
    [Range(1, 10)] [SerializeField] int minOneDieChance;
    [Range(1, 10)] [SerializeField] int cutHighLossChance;
    [Range(1,1000)][SerializeField] int workerDieRate;
    int worker = 1000;
    float progress = 0;
    float step = 0;
    float nextStep = 0;
    int lastPart = 0;
    Transform[] buildingChilds;
    List<Transform> buildingParts;
    GameManager gameManager;

    public int Worker
    {
        get
        {
            return worker;
        }
    }
    public void AddWorker(int count)
    {
        worker += count;
        SlaveCountDisplay.text = preText + worker;
    }
    public void RemoveWorker(int count)
    {
        worker -= count;
        SlaveCountDisplay.text = preText + worker;
    }

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        buildingParts = new List<Transform>();
        buildingChilds = building.GetComponentsInChildren<Transform>();
        foreach(Transform buildingChild in buildingChilds)
        {
            if (buildingChild.parent == building)
            {
                buildingParts.Add(buildingChild);
            }
        }
        foreach(Transform buildingPart in buildingParts)
        {
            buildingPart.gameObject.SetActive(false);
        }
        step = gesProgress / buildingParts.Count;
        nextStep = step;
        StartCoroutine(Build());
        SlaveCountDisplay.text = preText + worker;
    }

    IEnumerator Build()
    {
        while (true)
        {
            progress += progressPerWorker * worker;
            RemoveWorker(KillWorker());
            while (progress > nextStep)
            {
                buildingParts[lastPart].gameObject.SetActive(true);
                nextStep += step;
                lastPart++;
                if (lastPart >= buildingParts.Count)
                {
                    break;
                }
            }
            if (lastPart >= buildingParts.Count)
            {
                break;
            }
            yield return new WaitForSeconds(intervall);
        }
        gameManager.WinLevel();
    }

    private int KillWorker()
    {
        int dying = Mathf.RoundToInt(worker * workerDieRate * 0.1f);
        if (dying < 1)
        {
            int roll = Random.Range(0, 10);
            if (roll < minOneDieChance)
            {
                dying = 1;
            }
        }
        else if (dying > 1)
        {
            int roll = Random.Range(0, 10);
            if (roll < cutHighLossChance)
            {
                dying = Mathf.RoundToInt(dying * 0.5f);
            }
        }
        return dying;
    }
}
