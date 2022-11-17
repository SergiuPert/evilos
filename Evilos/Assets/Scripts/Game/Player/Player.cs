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

    private float lastAttack;
    private Animator animator;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameUIManager.Instance.spellSelected != 0 && Input.GetButtonDown("Fire1") && GameManager.Instance.gameRunning)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            CastSpell();
            GameUIManager.Instance.spellSelected = 0;
            lastAttack = Time.time;
        }
        else if (Input.GetButton("Fire1") && GameManager.Instance.gameRunning && GameUIManager.Instance.mana >= manaCost && lastAttack + attackSpeed < Time.time)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return; //needs testing on phone
            lastAttack = Time.time;
            Shoot(attackAnimation);
        }
        //if (Input.GetButtonDown("Fire2"))
        //{
        //    CastSpell();
        //    Debug.Log("Casted!");
        //}
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
        GameManager.Instance.extendedUserSave.SaveAmmoData();
        GameUIManager.Instance.UpdateAmmo();
        return true;
    }

    private void CastSpell()
    {
        bool hasScroll = ConsumeScroll();
        if (hasScroll)
        {
            animator.SetTrigger("Cast");
            GameObject spell = GameUIManager.Instance.spell;
            Vector3 position = new Vector3(0, -4, -0.1f);
            if (spell.CompareTag("CastAtClick"))
            {
                position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = -0.1f;
            }
            else if (spell.CompareTag("CastFromPlayer"))
            {
                position = new Vector3(-25, 0, -0.1f);
            }
            Quaternion rotation = new Quaternion(spell.transform.rotation.x, spell.transform.rotation.y, spell.transform.rotation.z, spell.transform.rotation.w);
            Instantiate(spell, position, rotation);
        }
    }
    private bool ConsumeScroll()
    {
        string spellName = GameUIManager.Instance.spell.gameObject.name;
        GameManager.Instance.extendedUserSave.Scrolls[spellName]--;
        if (GameManager.Instance.extendedUserSave.Scrolls[spellName] < 0)
        {
            GameManager.Instance.extendedUserSave.Scrolls[spellName] = 0;
            return false;
        }
        GameManager.Instance.extendedUserSave.SaveScrollsData();
        GameUIManager.Instance.UpdateScrolls();
        return true;
    }

}
