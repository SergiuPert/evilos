using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] magicMissiles;
    [SerializeField]
    private Transform firePoint;
    private Animator animator;
    //[SerializeField]
    //private GameObject[] magicMissiles;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && GameManager.Instance.gameRunning)
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Instantiate(magicMissiles[GameManager.Instance.missileIndex], firePoint.position, firePoint.rotation);
    }
}
