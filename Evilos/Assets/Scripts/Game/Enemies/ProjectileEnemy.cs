using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Projectile
{
    [SerializeField] protected float damage = 20;

    //private void FixedUpdate()
    //{
    //    transform.Translate(Vector3.left * speed);
    //}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Barrier barrier = collision.gameObject.GetComponent<Barrier>();
            if (barrier == null)
            {
                Debug.Log("Barrier script missing");
                return;
            }
            barrier.TakeDamage(damage);
            ActivateHitEffects();
        }
    }
}
