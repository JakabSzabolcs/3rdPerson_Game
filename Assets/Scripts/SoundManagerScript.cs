using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip shotMissed, shotHit, Shoot;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        shotMissed = Resources.Load<AudioClip>("fart");
        shotHit = Resources.Load<AudioClip>("hitsound");
        Shoot = Resources.Load<AudioClip>("gunsound");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string soundName)
    {
        switch(soundName)
        {
            case "shoot":
                audioSrc.PlayOneShot(Shoot);
                break;
            case "missed":
                audioSrc.PlayOneShot(shotMissed);
                break;
            case "hit":
                audioSrc.PlayOneShot(shotHit);
                break;
        }
    }
}
