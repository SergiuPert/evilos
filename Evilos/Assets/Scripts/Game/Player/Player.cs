using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject magicMissile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera Cam;
    [SerializeField] private string attackAnimation;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int manaCost;
    [SerializeField] private List<GameObject> spells;

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
            if (EventSystem.current.IsPointerOverGameObject()) return; //needs testing on phone
            lastAttack = Time.time;
            Shoot(attackAnimation);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            CastSpell();
            Debug.Log("Casted!");
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
        bool hasAmmo = true;
        if (manaCost == 0) 
        { 
            hasAmmo = ConsumeAmmo();
        }
        if (hasAmmo || manaCost > 0)
        {
            animator.SetTrigger(animation);
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        GameManager.Instance.extendedUserSave.Ammos[gameObject.name]--;
        if (GameManager.Instance.extendedUserSave.Ammos[gameObject.name] < 0)
        {
            GameManager.Instance.extendedUserSave.Ammos[gameObject.name] = 0;
            return false;
        }
        GameManager.Instance.extendedUserSave.SaveData();
        GameUIManager.Instance.UpdateAmmo();
        return true;

        //if (gameObject.name == "Fireblaster" && GameManager.Instance.userSave.FireblasterAmmo > 0)
        //{
        //    GameManager.Instance.userSave.FireblasterAmmo--;
        //}
        //else if (gameObject.name == "Frost Shard" && GameManager.Instance.userSave.FrostShardAmmo > 0)
        //{
        //    GameManager.Instance.userSave.FrostShardAmmo--;
        //}
        //else return false;
        //GameUIManager.Instance.UpdateAmmo();

        //return true;
    }

    private void CastSpell()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = -0.1f;
        Quaternion rotation = Quaternion.Euler(-30,0,0);
        Instantiate(spells[0], position, rotation);
    }
}
