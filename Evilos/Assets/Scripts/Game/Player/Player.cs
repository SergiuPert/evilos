using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject magicMissile;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Camera Cam;
    [SerializeField]
    private string attackAnimation;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private int manaCost;

    private float lastAttack;
    private Animator animator;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && GameManager.Instance.gameRunning && GameUIManager.Instance.mana >= manaCost && lastAttack + attackSpeed < Time.time)
        {
            lastAttack = Time.time;
            Shoot(attackAnimation);
        }
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
    public void AttackAnimationEvent()
    {
        RotateFirePoint();
        Instantiate(magicMissile, firePoint.position, firePoint.rotation);
        GameUIManager.Instance.mana -= manaCost;
        //ConsumeAmmo();
    }


    void Shoot(string animation)
    {
        bool hasAmmo = ConsumeAmmo();
        if (hasAmmo || manaCost > 0)
        {
            animator.SetTrigger(animation);
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("bruh");
            //Debug.Log(mousePos);
            if (mousePos.y > -7 && mousePos.y < 3)
            {
                animator.SetFloat("Aim", mousePos.y / 50);
            }
            else if (mousePos.y > 0)
            {
                animator.SetFloat("Aim", mousePos.y / 50 + Math.Abs(mousePos.x / 190));
            }
            else
            {
                animator.SetFloat("Aim", mousePos.y / 50 - Math.Abs(mousePos.x / 190));
            }
        }
    }
    private bool ConsumeAmmo()
    {
        switch(gameObject.name)
        {
            case "Fireblaster":
                if(GameManager.Instance.userSave.FireblasterAmmo > 0)
                {
                    GameManager.Instance.userSave.FireblasterAmmo--;
                }
                else
                {
                    return false;
                }
                break;
            case "Gun2":
                if(GameManager.Instance.userSave.Gun2Ammo > 0)
                {
                    GameManager.Instance.userSave.Gun2Ammo--;
                }
                else
                {
                    return false;
                }
                break;
            default:
                return false;
        }
        GameUIManager.Instance.UpdateAmmo();
        return true;
    }


}
