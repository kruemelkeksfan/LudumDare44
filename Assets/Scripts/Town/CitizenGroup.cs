using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenGroup : MonoBehaviour
{
    int citizenCount;
    public SpawnPoint spawnPoint;
    [SerializeField] int minCount = 2;
    [SerializeField] int maxCount = 25;
    [SerializeField] int displayTime = 15;

    public SlaveManager SlaveManager { get;  set; }

    private void Start()
    {
        StartCoroutine(Disappear());
    }

    public int ClaculateCitizenCount(float foodSliderValue)
    {
        citizenCount = Random.Range(minCount, maxCount + 1);
        citizenCount = Mathf.RoundToInt(foodSliderValue * citizenCount);
        return citizenCount;
    }

    private void OnMouseDown()
    {
        SlaveManager.AddSlaves(citizenCount);
        spawnPoint.CitizenGroup = null;
        Object.Destroy(gameObject);
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(displayTime);
        spawnPoint.CitizenGroup = null;
        Object.Destroy(gameObject);
    }




}
