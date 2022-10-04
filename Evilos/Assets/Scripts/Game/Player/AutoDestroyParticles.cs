using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticles : MonoBehaviour
{
    private float timeLeft = 1;
    private void Awake()
    {
        ParticleSystem system = GetComponent<ParticleSystem>();
        var main = system.main;
        timeLeft = main.startLifetimeMultiplier + main.duration;
        Destroy(gameObject, timeLeft);
    }
}
