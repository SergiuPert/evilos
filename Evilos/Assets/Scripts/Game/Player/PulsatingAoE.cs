using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingAoE : AoE
{
    [SerializeField] private float pulseInterval;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AoEPulse());
    }

    IEnumerator AoEPulse()
    {
        while (true)
        {
            DamageEnemies();
            yield return new WaitForSeconds(pulseInterval);
        }
    }
}
