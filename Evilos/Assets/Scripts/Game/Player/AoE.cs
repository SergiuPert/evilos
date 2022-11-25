using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoE : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float minDamage;
    [SerializeField] float maxDamage;
    [SerializeField] int critChance;
    [SerializeField] private float slowDegree = 0;
    [SerializeField] private float slowDuration = 0;
    [SerializeField] private float doT = 0;
    [SerializeField] private float doTDuration = 0;
    [SerializeField] private float delay = 0;
    void OnEnable()
    {
        //DamageEnemies();
        StartCoroutine(WaitForEffect());
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
                enemy.TakeDamage(damage, slowDegree, slowDuration);
                if (damage > 0)
                {
                    DamagePopup.Create(collider.transform.position, (int)damage, isCritical);
                }
                if (doT > 0)
                {
                    Debug.Log(enemy.gameObject.name);
                    enemy.StartCoroutine(enemy.DoT(doT, doTDuration));
                }
            }
        }
    }

    IEnumerator WaitForEffect()
    {
        yield return new WaitForSeconds(delay);
        DamageEnemies();
    }
}
