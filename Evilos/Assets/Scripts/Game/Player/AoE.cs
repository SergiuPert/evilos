using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoE : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float damage;
    void Start()
    {
        DamageEnemies();
    }

    private void DamageEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                DamagePopup.Create(collider.transform.position, (int)damage, false);
                enemy.TakeDamage(damage);
            }
        }
    }
}
