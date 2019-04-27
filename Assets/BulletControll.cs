using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{

    Rigidbody rigidbody;
    public int SlaveCount;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddRelativeForce(Vector3.forward * 50);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WorkingSite")
        {
            WorkingManager workingManager = other.gameObject.GetComponent<WorkingManager>();
            workingManager.AddWorker(SlaveCount);
            Object.Destroy(gameObject);
        }
        else if (other.tag == "Enemy")
        {
            //do damage
            Object.Destroy(gameObject);
        }

        
    }
}
