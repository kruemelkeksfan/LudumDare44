using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlaveManager : MonoBehaviour
{
    [SerializeField] SlaveShooter slaveShooter;
    [SerializeField] Text SlaveCountDisplay;
    [SerializeField] string preText = "free slave Count: ";

    int slaveCount = 0;
    [SerializeField] int initalSlaveCount;
    int slavesPerShoot = 1;
    int slavesPerBullet = 1;
    int bulletCount = 1;

    public int SlavesPerShoot
    {
        get
        {
            return slavesPerShoot;
        }
    }
    public int SlavesPerBullet
    {
        get
        {
            return slavesPerBullet;
        }
    }
    public int BulletCount
    {
        get
        {
            return bulletCount;
        }
    }

    public void SetSlavesPerShoot(int value)
    {
        slavesPerShoot = value;
    }
    public void SetSlavesPerBullet(int value)
    {
        slavesPerBullet = value;
    }
    public void SetBulletCount(int value)
    {
        bulletCount = value;
    }

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
        slaveShooter.isActive = false;
        slaveCount += count;
        SlaveCountDisplay.text = preText + slaveCount;
    }
    public void RemoveSlaves(int count)
    {
        slaveCount -= count;
        SlaveCountDisplay.text = preText + slaveCount;
    }
}
