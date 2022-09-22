using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMiner : MonoBehaviour
{
    private float speed = 4;
    private bool homeReached = false;
    private Animator animator;
    private float attackSpeed = 1;
    private float lastAttack = 0;
    private float damage = 10;
    private Home home;
    private float health = 100;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        home = GameObject.Find("Home").GetComponent<Home>();
        if (home == null)
        {
            Debug.Log("Home not found");
        }
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.Log("Game manager missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGameStart();
        if (health > 0 && gameManager.gameRunning)
        {
            if (!homeReached)
            {
                Move();
            }
            else
            {
                Attack();
            }
        }
    }

    void CheckForGameStart()
    {
        if (gameManager.gameRunning)
        {
            animator.SetTrigger("GameStart");
        }
    }

    private void Attack()
    {
        if (lastAttack + attackSpeed < Time.time)
        {
            
            animator.SetTrigger("Attack");
            lastAttack = Time.time;
        }
    }


    private void DamageHome()
    {
        if (home.health > 0)
        {
            home.TakeDamage(damage);
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Home"))
        {
            homeReached = true;
            animator.SetBool("Walking", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Home"))
        {
            homeReached = false;
            animator.SetBool("Walking", true);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.tag = "Untagged";
            animator.SetTrigger("Die");
        }
    }
}
