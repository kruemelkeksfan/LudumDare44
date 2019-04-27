using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlaveShooter : MonoBehaviour
{
    public bool isActive = false;
    [SerializeField] SlaveManager slaveManager;
    [SerializeField] GameObject citizen;

    Camera cam;
 float distance = 10.0f;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetButtonDown("ToggleFiremode"))
            {
                isActive = false;
            }
            if (Input.GetMouseButtonDown(1))
            {
                isActive = false;
            }
            else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (slaveManager.SlaveCount < slaveManager.SlavesPerShoot)
                {
                    isActive = false;
                    return;
                }
                Vector3 mousePos = Input.mousePosition;
                Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 400));
                int count = 0;

                while (count < slaveManager.BulletCount)
                {
                    
                    float rollX = Random.Range(0.1f, 2f);
                    float rollY = Random.Range(0.1f, 2f);
                    Vector3 position = new Vector3(Input.mousePosition.x + rollX, Input.mousePosition.y + rollY, distance);
                    position = Camera.main.ScreenToWorldPoint(position);

                    GameObject newGameObject = Instantiate(citizen, gameObject.transform);
                    BulletControll bulletControll = newGameObject.GetComponent<BulletControll>();
                    bulletControll.transform.LookAt(position);
                    bulletControll.SlaveCount = slaveManager.SlavesPerBullet;
                    count++;
                }
                slaveManager.RemoveSlaves(slaveManager.SlavesPerShoot);
            }
        }
        else if (Input.GetButtonDown("ToggleFiremode"))
        {
            isActive = true;
        }
    }
    
    public void Activate()
    {
        isActive = true;
    }
}
