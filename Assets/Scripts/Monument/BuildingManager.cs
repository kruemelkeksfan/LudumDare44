using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : WorkingManager
{
    [SerializeField] Text missingMaterialDisplay;
    [SerializeField] Transform building;
    [SerializeField] int intervall;
    [SerializeField] int gesProgress;
    [SerializeField] int progressPerWorker;
    [SerializeField] int materialPerWorker;
    [SerializeField] CollectorManager materailCollector;
    [SerializeField] AudioClip[] buildSfx;
    float progress = 0;
    float step = 0;
    float nextStep = 0;
    int lastPart = 0;
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
        WorkerCountDisplay.text = preText + worker;
    }

    IEnumerator Build()
    {
        while (true)
        {
            if (materailCollector.Material < worker * materialPerWorker)
            {
                int productiveWorker = Mathf.RoundToInt(materailCollector.Material / materialPerWorker);
                progress += progressPerWorker * productiveWorker;
                if (extraFoodToggle.isOn)
                {
                    progress = progress * extraProductionMulti;
                }
                missingMaterialDisplay.gameObject.SetActive(true);
                missingMaterialDisplay.text = "Missing: " + (materailCollector.Material - worker * materialPerWorker * -1);
                materailCollector.RemoveMaterial(productiveWorker * materialPerWorker);
            }
            else
            {
                missingMaterialDisplay.gameObject.SetActive(false);
                progress += progressPerWorker * worker;
                materailCollector.RemoveMaterial(worker * materialPerWorker);
            }
            RemoveWorker(KillWorker());
            while (progress > nextStep)
            {
                audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.clip = buildSfx[Random.Range(0, buildSfx.Length)];
                audioSource.Play();
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
}
