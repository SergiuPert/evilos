using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // to detach from parent and avoid being destroyed prematurely
    [SerializeField] private float effectTime;
    void Start()
    {
        transform.parent = null;
        Destroy(gameObject, effectTime);
    }
}
