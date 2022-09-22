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
    [SerializeField]
    private int missileIndex = 0;
    private Animator animator;
    GameManager gameManager;
    //[SerializeField]
    //private GameObject[] magicMissiles;


    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.Log("Game manager missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && gameManager.gameRunning)
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Instantiate(magicMissiles[missileIndex], firePoint.position, firePoint.rotation);
    }
}
