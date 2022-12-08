using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : Projectile
{
    [SerializeField] protected float minDamage = 20;
    [SerializeField] protected float maxDamage = 50;
    [SerializeField] protected int critChance = 25;
    [SerializeField] protected float slowDegree = 0;
    [SerializeField] protected float slowDuration = 0;
    [SerializeField] private float doT = 0;
    [SerializeField] private float doTDuration = 0;
    [SerializeField] protected GameObject aoE;

    protected float damage;
    protected override void Start()
    {
        base.Start();
        int upgradeLevel = GameManager.Instance.userSave.MainWeaponUpgrade % 6;
        minDamage *= (1 + 0.1f * upgradeLevel);
        maxDamage *= (1 + 0.1f * upgradeLevel);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.Log("Enemy script missing");
                return;
            }
            damage = Random.Range(minDamage, maxDamage);
            bool isCritical = Random.Range(0, 100) < critChance;
            if (isCritical) damage *= 2;
            enemy.TakeDamage(damage, slowDegree, slowDuration);
            DamagePopup.Create(transform.position, (int)damage, isCritical);
            if (aoE != null)
            {
                aoE.gameObject.SetActive(true);
            }
            if (doT > 0)
            {
                enemy.StartCoroutine(enemy.DoT(doT, doTDuration));
            }
            ActivateHitEffects();
        }

    }
}
