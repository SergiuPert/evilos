using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4;
    protected bool inRange = false;
    protected Animator animator;
    [SerializeField]
    private float attackSpeed = 1;
    private float lastAttack = 0;
    [SerializeField]
    private float damage = 10;
    [SerializeField]
    private float health = 100;
    [SerializeField]
    private int goldValue = 10;
    //[SerializeField]
    private Home home;
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        //animator.SetTrigger("GameStop");
        //GameUIManager.startGame += StartGame;
        home = GameObject.Find("Home").GetComponent<Home>();
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

    private void StartGame()
    {
        ////animator = GetComponent<Animator>();
        //if (animator == null)
        //{
        //    Debug.Log(this);
        //    animator = GetComponentInChildren<Animator>();
        //}
        //Debug.Log(animator);
        animator.SetTrigger("GameStart");
        //Debug.Log("Second");
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
        if (home.health > 0)
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
        if (health <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.tag = "Untagged";
            animator.SetTrigger("Die");
        }
    }
}
