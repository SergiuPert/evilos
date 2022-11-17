using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private float effectTime;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        Destroy(gameObject, effectTime);
    }
}
