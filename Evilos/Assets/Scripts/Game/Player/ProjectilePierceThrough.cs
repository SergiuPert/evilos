using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePierceThrough : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    [SerializeField] private float critChance;
    
    
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed); //time delta time
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.Log("Enemy script missing");
                return;
            }
            float damage = Random.Range(minDamage, maxDamage);
            bool isCritical = Random.Range(0, 100) < critChance;
            if (isCritical) damage *= 2;
            enemy.TakeDamage(damage);
            DamagePopup.Create(collision.transform.position, (int)damage, isCritical);
        }
    }

}
