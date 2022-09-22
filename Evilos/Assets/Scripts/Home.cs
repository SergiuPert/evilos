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
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager != null)
        {
            gameManager.Lose();
        }
        else
        {
            Debug.Log("Game manager mising");
        }
    }
}
