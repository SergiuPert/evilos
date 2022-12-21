using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy
{
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float timeBetweenActions;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int weaponSway;
    //[SerializeField] private string idleAnimation;

    private bool inMinRange = false;
    private bool isActing = false;
    private float lastAction;
    private Transform target;

    new private void Start()
    {
        base.Start();
        target = GameObject.Find("Target").transform;
    }

    new private void Update()
    {
        base.Update();
        if (!isActing)
        {
            CheckMaxRange();
        }
    }

    protected override void Action()
    {
        if (!inMinRange) // also check min range after a knockback
        {
            if (lastAction + timeBetweenActions < Time.time)
            {
                GetCloserOrShoot();
                lastAction = Time.time;
            }
        }
        else
        {
            Shoot();
        }
    }

    private bool CheckMinRange()
    {
        Vector3 rangeStart = transform.position + new Vector3(-4, 7, 0.1f);
        RaycastHit2D hit = Physics2D.Raycast(rangeStart, transform.right, minRange, layerMask);
        return hit;
    }

    private void CheckMaxRange()
    {
        Vector3 rangeStart = transform.position + new Vector3(-4, 7, 0.1f);
        RaycastHit2D hit = Physics2D.Raycast(rangeStart, transform.right, maxRange, layerMask);
        //Debug.DrawRay(rangeStart, transform.right * maxRange, Color.red);
        if (hit && hit.collider.CompareTag("Barrier"))
        {
            inRange = true;
            animator.SetBool("Walking", false);
        }
        else
        {
            inRange = false;
            animator.SetBool("Walking", true);
        }
    }
    private void GetCloserOrShoot()
    {
        int choice = Random.Range(0, 100);
        if (choice < 50)
        {
            StartCoroutine(GetCloser());
        }
        else
        {
            Shoot();
        }
    }

    IEnumerator GetCloser()
    {
        animator.SetBool("Walking", true);
        isActing = true;
        inRange = false;
        yield return new WaitForSeconds(timeBetweenActions);
        isActing = false;
        animator.SetBool("Walking", false);
        inMinRange = CheckMinRange();
        Debug.Log(inMinRange);
    }

    protected virtual void Shoot()
    {
        if (lastAttack + attackSpeed < Time.time)
        {
            StartCoroutine(AimAndFire());
        } // TODO: Play some animation if you cant attack
    }

    IEnumerator AimAndFire()
    {
        animator.SetBool("Aiming", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("Shoot"); //optimize, make attack and shoot the same
        lastAttack = Time.time;
        animator.SetBool("Aiming", false);
    }

    private void RotateFirePoint()
    {
        int targetOffset = Random.Range(-weaponSway, weaponSway);
        Vector3 t = target.position;
        t.y += targetOffset;
        firePoint.LookAt(t);
    }

    public void CreateProjectile()
    {
        RotateFirePoint();
        Instantiate(projectile, firePoint.position, firePoint.rotation);
    }
}
