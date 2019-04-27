using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkingManager : MonoBehaviour
{
    [SerializeField] protected Text WorkerCountDisplay;
    [SerializeField] protected string preText = "working Count: ";
    [Header("Dying Worker")]
    [SerializeField] int highDeathCount = 100;
    [Range(1, 10)] [SerializeField] int minOneDieChance;
    [Range(1, 10)] [SerializeField] int cutHighLossChance;
    [Range(1,1000)][SerializeField] int workerDieRate;
    protected int worker;

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
        WorkerCountDisplay.text = preText + worker;
    }
    public void RemoveWorker(int count)
    {
        worker -= count;
        WorkerCountDisplay.text = preText + worker;
    }

    protected int KillWorker()
    {
        int dying = Mathf.RoundToInt(worker * workerDieRate * 0.1f);
        if (dying < 1)
        {
            int roll = Random.Range(0, 10);
            if (roll < minOneDieChance)
            {
                dying = 1;
            }
            else
            {
                dying = 0;
            }
        }
        else if (dying > highDeathCount)
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
