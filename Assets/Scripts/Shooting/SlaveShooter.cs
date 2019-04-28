using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlaveShooter : MonoBehaviour
{
    bool isActive = false;
    [SerializeField] GameObject ShootingDisplay;
    [SerializeField] AudioClip[] screamSfx;
    [SerializeField] AudioClip splashSfx;
    [SerializeField] SlaveManager slaveManager;
    [SerializeField] GameObject citizen;
    [SerializeField] int fireIntervall = 1;
    [SerializeField] Text freedSlavesDisplay;
    int count = 0;
   
    Camera cam;
    float distance = 10.0f;
    float nextFireTime;

    public AudioClip GetScreamSfx()
    {
        int roll = Random.Range(0, screamSfx.Length);
        return screamSfx[roll];
    }
    public AudioClip GetSplashSfx()
    {
        return splashSfx;
    }
    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            ShootingDisplay.SetActive(value);
            isActive = value;
        }
    }
    public void AddFreedSlaves(int count)
    {
        this.count += count;
        freedSlavesDisplay.text = "Slaves freed: " + this.count;
    }
    void Start()
    {
        freedSlavesDisplay.text = "Slaves freed: " + count;
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (IsActive)
        {
            if (Input.GetButtonDown("ToggleFiremode"))
            {
                IsActive = false;
            }
            if (Input.GetMouseButtonDown(1))
            {
                IsActive = false;
            }
            else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (slaveManager.SlaveCount < slaveManager.SlavesPerShoot)
                {
                    IsActive = false;
                    return;
                }
                if (Time.time < nextFireTime)
                {
                    return;
                }
                nextFireTime = Time.time + fireIntervall;
                Vector3 mousePos = Input.mousePosition;
                Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 400));
                int count = 0;

                while (count < slaveManager.BulletCount)
                {
                    float rollX = Random.Range(0.1f, 2f);
                    float rollY = Random.Range(0.1f, 2f);
                    Vector3 position = new Vector3(Input.mousePosition.x + rollX, Input.mousePosition.y + rollY, distance);
                    position = Camera.main.ScreenToWorldPoint(position);

                    GameObject newGameObject = Instantiate(citizen, gameObject.transform.position, Quaternion.identity);
                    BulletControll bulletControll = newGameObject.GetComponent<BulletControll>();
                    bulletControll.transform.LookAt(position);
                    bulletControll.slaveCount = slaveManager.SlavesPerBullet;
                    bulletControll.slaveShooter = this;
                    count++;
                }
                slaveManager.RemoveSlaves(slaveManager.SlavesPerShoot);
            }
        }
        else if (Input.GetButtonDown("ToggleFiremode"))
        {
            IsActive = true;
        }
    }
    
    public void Activate()
    {
        IsActive = true;
    }
}
