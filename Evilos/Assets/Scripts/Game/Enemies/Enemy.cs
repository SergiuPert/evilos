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

    public float slow = 0;
    protected bool inRange = false;
    protected Animator animator;
    private Barrier home;
    private float lastAttack = 0;
    private float maxHealth;

    void Start()
    {
        GameUIManager.startGame += StartGame;
        GameUIManager.stopGame += StopGame;
        gameObject.GetComponent<SortingGroup>().sortingOrder = (int)(-transform.position.y);
        maxHealth = health;
        animator = GetComponent<Animator>();
        
        home = GameObject.Find("Home").GetComponent<Barrier>(); // create event for home to take damage
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
            float movementSpeed = speed - slow;
            if (movementSpeed < 0)
            {
                movementSpeed = 0;
            }
            animator.speed = movementSpeed / speed; // optimize if possible
            if (!inRange)
            {
                Move(movementSpeed);
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

    private void Move(float movementSpeed)
    {
        
        transform.Translate(Vector2.right * (movementSpeed) * Time.deltaTime);
    }
    public void TakeDamage(float damage, float slowDegree, float slowDuration)
    {
        health -= damage;
        healthBar.value = health/maxHealth;
        if (slowDegree != 0)
        {
            StartCoroutine(SlowStatus(slowDegree, slowDuration));
        }
        if (health <= 0)
        {
            animator.speed = 1;
            Destroy(healthBar.transform.parent.gameObject);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.tag = "Untagged";
            animator.SetBool("Dead", true);
            animator.SetTrigger("Die");
            GameUIManager.Instance.UpdateGold(goldValue);
            GameUIManager.Instance.CheckForWin();
        }
    }

    IEnumerator SlowStatus(float slowDegree, float slowDuration)
    {
        float slowAmmount = speed * slowDegree;
        //if (slow >= slowAmmount)
        //{
        //    slowAmmount = slow * slowDegree;
        //}
        slow += slowAmmount;
        yield return new WaitForSeconds(slowDuration);
        slow -= slowAmmount;
    }

    public IEnumerator DoT(float doT, float doTDuration)
    {
        while (doTDuration > 0 && health > 0)
        {
            TakeDamage(doT, 0, 0);
            Vector3 position = transform.position;
            position.y += 7;
            DamagePopup.Create(position, (int)doT, false);
            yield return new WaitForSeconds(1);
            doTDuration -= 1;
        }
    }
}
