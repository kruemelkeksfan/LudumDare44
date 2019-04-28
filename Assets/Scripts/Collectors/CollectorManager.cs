using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectorManager : WorkingManager
{
    [SerializeField] protected Text materialCountDisplay;
    [SerializeField] protected string materialText;
    [SerializeField] int materialPerWorker;
    [SerializeField] int intervall;
    [SerializeField] int startingMaterial;
    [SerializeField] int criticalValue = 5;
    [SerializeField] Color criticalColor;
    Color normalColor;
    FoodManager foodManager;

    protected int material;

    public int Material
    {
        get
        {
            return material;
        }
    }
    public void AddMaterial(int count)
    {
        material += count;
        materialCountDisplay.text = materialText + material;
    }
    public void RemoveMaterial(int count)
    {
        if (count < 0)
        {
            return;
        }
        material -= count;
        if (material <= criticalValue)
        {
            materialCountDisplay.color = criticalColor;
        }
        else
        {
            materialCountDisplay.color = normalColor;
        }
        materialCountDisplay.text = materialText + material;
    }

    private void Start()
    {
        normalColor = materialCountDisplay.color;
        worker = startingWorkforce;
        material = startingMaterial;
        WorkerCountDisplay.text = preText + worker;
        materialCountDisplay.text = materialText + material;
        foodManager = GetComponent<FoodManager>();
        StartCoroutine(Collect());
    }

    protected IEnumerator Collect()
    {
        yield return new WaitForSeconds(intervall);
        while (true)
        {
            int newMaterial = materialPerWorker * worker;
            if (extraFoodToggle.isOn)
            {
                newMaterial = Mathf.RoundToInt(newMaterial * extraProductionMulti);
            }
            AddMaterial(newMaterial);
            RemoveWorker(KillWorker());
            if(foodManager!= null)
            {
                foodManager.CalculateFood(newMaterial);
            }
            yield return new WaitForSeconds(intervall);
        }
    }
}
