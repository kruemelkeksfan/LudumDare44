using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlaveManager : MonoBehaviour
{
    [SerializeField] Text SlaveCountDisplay;

    int slaveCount = 0;
    [SerializeField] int initalSlaveCount;

    private void Start()
    {
        slaveCount = initalSlaveCount;
        SlaveCountDisplay.text = "Slave Count: " + slaveCount;
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
        SlaveCountDisplay.text = "Slave Count: " + slaveCount;
    }
    public void RemoveSlaves(int count)
    {
        slaveCount -= count;
        SlaveCountDisplay.text = "Slave Count: " + slaveCount;
    }
}
