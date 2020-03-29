using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    public float speed; 
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }
}
