using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 15f;
    [SerializeField]
    private float hitOffset = 0f;
    [SerializeField]
    private GameObject hit;
    [SerializeField]
    private GameObject flash;
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject[] Detached;
    [SerializeField]
    private float damage = 50;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (flash != null)
        {
            //Instantiate flash effect on projectile position
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;

            //Destroy flash effect depending on particle Duration time
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject, 20);
    }

    void FixedUpdate()
    {
        if (speed != 0)
        {
            rb.velocity = transform.forward * speed;     
        }
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
            enemy.TakeDamage(damage);
            //Lock all axes movement and rotation
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            speed = 0;

            //Spawn hit effect on collision
            if (hit != null)
            {
                var hitInstance = Instantiate(hit, transform.position, transform.rotation);

                //Destroy hit effects depending on particle Duration time
                var hitPs = hitInstance.GetComponent<ParticleSystem>();
                if (hitPs != null)
                {
                    Destroy(hitInstance, hitPs.main.duration);
                }
                else
                {
                    var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitInstance, hitPsParts.main.duration);
                }
            }

            //Removing trail from the projectile on cillision enter or smooth removing. Detached elements must have "AutoDestroying script"
            foreach (var detachedPrefab in Detached)
            {
                if (detachedPrefab != null)
                {
                    detachedPrefab.transform.parent = null;
                }
            }
            //Destroy projectile on collision
            Destroy(gameObject);
        }

    }
}
