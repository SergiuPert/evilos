using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public float health;
    void Start()
    {
        health = 100 + GameManager.Instance.additionalHealth;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        GameUIManager.Instance.Lose();
    }
}
