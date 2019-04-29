using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneClickShootActive : MonoBehaviour
{
    [SerializeField] SlaveShooter slaveShooter;
    private void OnMouseDown()
    {
        slaveShooter.Activate();
    }
}
