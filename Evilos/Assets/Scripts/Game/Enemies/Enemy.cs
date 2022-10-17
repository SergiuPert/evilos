using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4;
    [SerializeField]
    private float attackSpeed = 1;
    [SerializeField]
    private float damage = 10;
    [SerializeField]
    private float health = 100;
    [SerializeField]
    private int goldValue = 10;
    [SerializeField]
    private Slider healthBar;


    protected bool inRange = false;
    protected Animator animator;
    private Home home;
    private float lastAttack = 0;
    private float maxHealth;

    void Start()
    {
        GameUIManager.startGame += StartGame;
        GameUIManager.stopGame += StopGame;
        gameObject.GetComponent<SortingGroup>().sortingOrder = (int)(-transform.position.y);
        maxHealth = health;
        animator = GetComponent<Animator>();
        
        home = GameObject.Find("Home").GetComponent<Home>(); // create event for home to take damage
        if (home == null)
        {
            Debug.Log("Home not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0 && GameManager.Instance.gameRunning)
        {
            if (!inRange)
            {
                Move();
            }
            else
            {
                Attack();
            }
        }
    }

    public void StartGame()
    {
        animator.SetTrigger("GameStart");
    }
    private void StopGame()
    {
        animator.SetTrigger("GameStop");
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
        if (inRange)
        {
            home.TakeDamage(damage);
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health/maxHealth;
        if (health <= 0)
        {
            Destroy(healthBar.transform.parent.gameObject);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.tag = "Untagged";
            animator.SetBool("Dead", true);
            animator.SetTrigger("Die");
            GameUIManager.Instance.UpdateGold(goldValue);
        }
    }
}
