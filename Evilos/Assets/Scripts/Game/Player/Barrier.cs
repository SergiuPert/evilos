using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private float health;
    private float maxHealth;

    void Start()
    {
        health = 100 + GameManager.Instance.additionalHealth;
        maxHealth = health;
        GameUIManager.Instance.UpdateHealth(health, maxHealth);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        GameUIManager.Instance.UpdateHealth(health, maxHealth);
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
