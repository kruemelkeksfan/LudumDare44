using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkingManager : MonoBehaviour
{
    [SerializeField] protected Text WorkerCountDisplay;
    [SerializeField] protected string preText = "working Count: ";
    [SerializeField] protected int startingWorkforce;
    [Header("Dying Worker")]
    [SerializeField] int highDeathCount = 100;
    [Range(1, 10)] [SerializeField] int minOneDieChance;
    [Range(1, 10)] [SerializeField] int cutHighLossChance;
    [Range(1,1000)][SerializeField] int workerDieRate;
    protected int worker;
    [SerializeField] protected bool noNegativeWorkers; 

    public int Worker
    {
        get
        {
            if (worker > 0)
            {
                return worker;
            }
            return 0;
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
        if (noNegativeWorkers && worker < 0)
        {
            worker = 0;
        }
        WorkerCountDisplay.text = preText + worker;
    }

    protected int KillWorker()
    {
        int dying = Mathf.RoundToInt(worker * ((workerDieRate * 0.1f)/100));
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
        if (noNegativeWorkers && Worker-dying < 0)
        {
            dying = 0;
        }
        return dying;
    }
}
