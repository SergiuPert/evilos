using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoE : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float minDamage;
    [SerializeField] float maxDamage;
    [SerializeField] int critChance;
    void Start()
    {
        DamageEnemies();
    }

    protected void DamageEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider in colliders)
        {
            float damage = Random.Range(minDamage, maxDamage);
            bool isCritical = Random.Range(0, 100) < critChance;
            if (isCritical) damage *= 2;
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                DamagePopup.Create(collider.transform.position, (int)damage, isCritical);
                enemy.TakeDamage(damage);
            }
        }
    }
}
