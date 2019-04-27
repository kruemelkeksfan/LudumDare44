using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlaveManager : MonoBehaviour
{
    [SerializeField] Text SlaveCountDisplay;
    [SerializeField] string preText = "free slave Count: ";

    int slaveCount = 0;
    [SerializeField] int initalSlaveCount;

    private void Start()
    {
        slaveCount = initalSlaveCount;
        SlaveCountDisplay.text = preText + slaveCount;
    }

    public int SlaveCount
    {
        get
        {
            return slaveCount;
        }
    }
    public void AddSlaves(int count)
    {
        slaveCount += count;
        SlaveCountDisplay.text = preText + slaveCount;
    }
    public void RemoveSlaves(int count)
    {
        slaveCount -= count;
        SlaveCountDisplay.text = preText + slaveCount;
    }
}
