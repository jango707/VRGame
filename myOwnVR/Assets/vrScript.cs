using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vrScript : MonoBehaviour
{
    
        public Camera cam;

        void Update()
        {
            cam.transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, 0);
        Debug.Log(-Input.gyro.rotationRateUnbiased.x );
        Debug.Log(-Input.gyro.rotationRateUnbiased.y);
        }
    }

