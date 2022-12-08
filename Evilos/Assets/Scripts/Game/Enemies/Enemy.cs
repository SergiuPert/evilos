using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float speed = 4;
    [SerializeField]
    protected float attackSpeed = 1;
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
    protected Barrier barrier;
    protected float lastAttack = 0;
    private float maxHealth;

    protected void Start()
    {
        GameUIManager.startGame += StartGame;
        GameUIManager.stopGame += StopGame;
        gameObject.GetComponent<SortingGroup>().sortingOrder = (int)(-transform.position.y);
        maxHealth = health;
        animator = GetComponent<Animator>();
        
        barrier = GameObject.Find("Barrier").GetComponent<Barrier>(); // create event for home to take damage
        if (barrier == null)
        {
            Debug.Log("Barrier not found");
        }
    }

    // Update is called once per frame
    protected void Update()
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
                Action();
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartAnimation(1f));
    }

    IEnumerator StartAnimation(float maxWait)
    {
        float time = Random.Range(0f, maxWait);
        yield return new WaitForSeconds(time);
        animator.SetTrigger("GameStart");
    }

    private void StopGame()
    {
        animator.SetTrigger("GameStop");
    }
    protected void Attack()
    {
        if (lastAttack + attackSpeed < Time.time)
        {
            animator.SetTrigger("Attack");
            lastAttack = Time.time;
        }
    }

    protected virtual void Action()
    {

    }

    private void DamageBarrier()
    {
        if (inRange)
        {
            barrier.TakeDamage(damage);
        }
    }

    protected void Move(float movementSpeed)
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
