using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroEnabled : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

  

    public float speed = 1f;
    private Camera cam;
    public Color color1 = Color.red;
    public Color color2 = Color.blue;
    public float duration = 3.0F;

    private void Start()
    {

        cam = GetComponent<Camera>();
        
    cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }
    private void Update()
    {
       
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }

        transform.position += new Vector3(0,0,speed*Time.deltaTime);
        float t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = Color.Lerp( color1, color2, t);
    }

}
