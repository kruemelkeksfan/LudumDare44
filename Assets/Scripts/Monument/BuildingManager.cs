using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : WorkingManager
{
    [SerializeField] Text missingMaterialDisplay;
    [SerializeField] Transform building;
    [SerializeField] int gesProgress;
    [SerializeField] int progressPerWorker;
    [SerializeField] int materialPerWorker;
    [SerializeField] CollectorManager materailCollector;
    [SerializeField] CombatManager combatManager;
    [SerializeField] AudioClip[] buildSfx;
    float progress = 0;
    float lastStep = 0;
    float step = 0;
    float nextStep = 0;
    int lastPart = 1;
    bool waiting;
    Transform[] buildingChilds;
    List<Transform> buildingParts;
    GameManager gameManager;
    AudioSource audioSource;

    void Start()
    {
        worker = startingWorkforce;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        buildingParts = new List<Transform>();
        buildingChilds = building.GetComponentsInChildren<Transform>();
        foreach (Transform buildingChild in buildingChilds)
        {
            if (buildingChild.parent == building)
            {
                buildingParts.Add(buildingChild);
            }
        }
        foreach (Transform buildingPart in buildingParts)
        {
            buildingPart.gameObject.SetActive(false);
        }
        step = gesProgress / buildingParts.Count;
        nextStep = step;
        buildingParts[0].gameObject.SetActive(true);
        WorkerCountDisplay.text = preText + worker;
    }

    public void LossProgress(float value)
    {
        progress -= value;
        while (progress < lastStep)
        {
            if (lastPart == 1)
            {
                break;
            }
            lastPart--;
            buildingParts[lastPart].gameObject.SetActive(false);
            nextStep = lastStep;
            lastStep -= step;
        }
    }

    public void Build()
    {
        float progressGain;
        if (materailCollector.Material < worker * materialPerWorker)
        {
            int productiveWorker = Mathf.RoundToInt(materailCollector.Material / materialPerWorker);

            progressGain = progressPerWorker * productiveWorker;
            if (extraFoodToggle.isOn)
            {
                progressGain = progressGain * extraProductionMulti;
            }
            missingMaterialDisplay.gameObject.SetActive(true);
            missingMaterialDisplay.text = "Missing: " + (materailCollector.Material - worker * materialPerWorker * -1);
            materailCollector.RemoveMaterial(productiveWorker * materialPerWorker);
        }
        else
        {
            missingMaterialDisplay.gameObject.SetActive(false);
            progressGain = progressPerWorker * worker;
            materailCollector.RemoveMaterial(worker * materialPerWorker);
        }
        RemoveWorker(KillWorker());
        if (progressGain < 0)
        {
            LossProgress(progressGain * -1f);
        }
        else
        {
            progress += progressGain;
            if (progress > nextStep)
            {
                audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.clip = buildSfx[Random.Range(0, buildSfx.Length)];
                audioSource.Play();
                combatManager.RollForAttack();
                buildingParts[lastPart].gameObject.SetActive(true);
                lastStep = nextStep;
                nextStep += step;
                lastPart++;
            }
            if (lastPart >= buildingParts.Count && !waiting)
            {
                if (combatManager.combat)
                {
                    waiting = true;
                    StartCoroutine(CheckForCombat());
                }
                else
                {
                    gameManager.WinLevel();
                }
            }
        }
    }
    IEnumerator CheckForCombat()
    {
        yield return new WaitWhile(() => combatManager.combat);
        waiting = false;
        if (lastPart >= buildingParts.Count)
        {
            gameManager.WinLevel();
        }
    }
}
