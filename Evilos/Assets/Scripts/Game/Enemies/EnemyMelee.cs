using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            inRange = true;
            animator.SetBool("Walking", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            inRange = false;
            animator.SetBool("Walking", true);
        }
    }

    protected override void Action()
    {
        Attack();
    }
}
