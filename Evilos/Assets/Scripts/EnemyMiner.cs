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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        home = GameObject.Find("Home").GetComponent<Home>();
        if (home == null)
        {
            Debug.Log("Home not found");
        }
    }

    // Update is called once per frame
    void Update()
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
        home.TakeDamage(damage);
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




    //void OnAnimatorMove()
    //{
    //    Debug.Log("Moving");
    //    transform.Translate(Vector2.right* speed * Time.deltaTime);
    //}

}
