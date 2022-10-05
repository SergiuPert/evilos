using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private float health;

    void Start()
    {
        health = 100 + GameManager.Instance.additionalHealth;
        GameUIManager.Instance.UpdateHealth((int)health);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        GameUIManager.Instance.UpdateHealth((int)health);
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
