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
        worker = startingWorkforce;
        material = startingMaterial;
        normalColor = materialCountDisplay.color;
        WorkerCountDisplay.text = preText + worker;
        materialCountDisplay.text = materialText + material;
        StartCoroutine(Collect());
    }

    protected IEnumerator Collect()
    {
        while (true)
        {
            AddMaterial(materialPerWorker * worker);
            RemoveWorker(KillWorker());
            yield return new WaitForSeconds(intervall);
        }
    }
}
