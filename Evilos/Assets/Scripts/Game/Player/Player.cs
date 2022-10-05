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
    private Camera Cam;

    private Animator animator;

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
        RotateFirePoint();
        Instantiate(magicMissiles[GameManager.Instance.missileIndex], firePoint.position, firePoint.rotation);
    }

    private void RotateFirePoint()
    {
        if (Cam != null)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            firePoint.rotation = Quaternion.LookRotation(mousePos - firePoint.position);
        }
        else
        {
            Debug.Log("No camera");
        }
    }

}
