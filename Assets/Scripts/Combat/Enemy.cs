using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] float maxSidetrack = 0.03f;
    CombatManager combatManager;
    int count = 0;
    bool decided = false;
    Vector3 direction;
    int maxCount = 0;

    private void Start()
    {
        combatManager = gameObject.GetComponentInParent<CombatManager>();
    }
    private void Update()
    {
        if (combatManager.pause)
        {
            return;
        }
        transform.position += Vector3.forward * Time.deltaTime * speed;
        if (count < maxCount)
        {
            SideWalk();
        }
        else
        {
            int roll = Random.Range(0, 3);
            maxCount = Random.Range(0, 15);
            if (roll == 1 && transform.localPosition.x < maxSidetrack)
            {
                direction = Vector3.right;
            }
            else if (roll == 2 && transform.localPosition.x > -maxSidetrack)
            {
                direction = Vector3.left;
            }
            else
            {
                direction = Vector3.forward;
            }
        }
    }

    void SideWalk()
    {
        if (transform.localPosition.x > -maxSidetrack && transform.localPosition.x < maxSidetrack)
        {
            transform.position += direction * Time.deltaTime * speed;
        }
        else
        {
            count = maxCount;
        }
    }


    public void BeenHit ()
    {
        combatManager.enemies.Remove(this);
        Object.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            combatManager.Hit(this);
            Object.Destroy(gameObject);
        }
    }
}
