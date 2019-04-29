using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] float maxSidetrack = 0.03f;
    CombatManager combatManager;
    int count = 0;
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
        transform.position += Vector3.forward * 2 * Time.deltaTime * speed;
        if (count < maxCount)
        {
            SideWalk();
        }
        else
        {
            count = 0;
            int roll = Random.Range(0, 3);
            maxCount = Random.Range(15, 50);
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
                direction = Vector3.forward * 2;
            }
        }
    }

    void SideWalk()
    {
        if (transform.localPosition.x > -maxSidetrack && transform.localPosition.x < maxSidetrack)
        {
            transform.position += direction * Time.deltaTime * speed;
            count++;
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
