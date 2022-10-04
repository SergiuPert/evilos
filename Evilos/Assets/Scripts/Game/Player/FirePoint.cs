using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField]
    private Camera Cam;
    void Update()
    {
        if (Cam != null)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.rotation = Quaternion.LookRotation(mousePos - transform.position); // might have to make a function out of this and call it from the player before shooting
        }
        else
        {
            Debug.Log("No camera");
        }
    }
}
