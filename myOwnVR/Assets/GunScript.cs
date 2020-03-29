using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject flash;
    public Transform loc;
    public Transform locBullet;
    public Light light;
    public GameObject bullet;

    public AudioSource audio;
    AudioClip microphoneInput;
    bool microphoneInitialized;
    public float sensitivity;
    public bool flapped;

    private void Awake()
    {
        //init microphone input
        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(Microphone.devices[0], true, 999, 44100);
            microphoneInitialized = true;
        }
    }

    private void Start()
    {
        bullet.transform.position = new Vector3(0.2f, 0.8f, -3f);


        //audio.clip = Microphone.Start("", true, 10, 33000);
        //audio.loop = true;
        //while (!(Microphone.GetPosition(null) > 0)) { }
       
        //audio.Play();
        //Debug.Log(Microphone.IsRecording(Microphone.devices[0]).ToString());
        //Debug.Log(Microphone.devices[0].ToString());

        if (Microphone.GetPosition(Microphone.devices[0]) > 0) Debug.Log("Started");
    }
    void Update()
    {
        //get mic volume
        int dec = 128;
        float[] waveData = new float[dec];
        int micPosition = Microphone.GetPosition(null) - (dec + 1); // null means the first microphone
        microphoneInput.GetData(waveData, micPosition);

        // Getting a peak on the last 128 samples
        float levelMax = 0;
        for (int i = 0; i < dec; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        float level = Mathf.Sqrt(Mathf.Sqrt(levelMax));
        //Debug.Log(level.ToString());
       // if (level > 0.4) Debug.Log("HEY");
    
        //bullet.transform.position += new Vector3(0, 0, 1 * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) || level>0.4)
        {
            shoot();
        }
    }

    public void shoot()
    {
        GameObject bullets = Instantiate(bullet, locBullet);
        bullets.transform.parent = null;

        Destroy(bullets, 5f);

        bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 10f);

       GameObject muzzle = Instantiate(flash, loc);
        Destroy(muzzle, 0.2f);
        StartCoroutine(MuzzleFlashCR());

    }
    private IEnumerator MuzzleFlashCR()
    {
        light.enabled = true;
        yield return new WaitForSeconds(0.5f);
        light.enabled = false;
    }
}
