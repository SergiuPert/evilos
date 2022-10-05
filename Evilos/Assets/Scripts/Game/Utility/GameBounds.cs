using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBounds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            collision.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(collision.gameObject, 3);
            return;
        }
        if (!collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
