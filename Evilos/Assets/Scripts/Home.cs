using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public float health = 100;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("took " + damage + " damage");
        Death();
    }

    void Death()
    {
        if (health <= 0)
        {
            Debug.Log("ded");
        }
    }
}