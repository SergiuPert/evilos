using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("started!!!!");
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.right * 30, Color.red);
    }
    private void OnDestroy()
    {
        //Debug.Log("Destroyed");
    }
}
