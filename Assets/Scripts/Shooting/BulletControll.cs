using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rigidbody;
    public SlaveShooter slaveShooter;
    public int SlaveCount;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = slaveShooter.GetScreamSfx();
        audioSource.Play();
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
            audioSource.Stop();
            audioSource.spatialBlend = 0;
            audioSource.clip = slaveShooter.GetSplashSfx();
            audioSource.Play();
            Object.Destroy(gameObject, 2);
        }
        else if (other.tag == "Enemy")
        {
            //do damage
            Object.Destroy(gameObject, 2);
        }

        
    }
}
