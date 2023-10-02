using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmContinue : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.time = PlayerPrefs.GetFloat("audioTime", 0f);
        audioSource.volume = PlayerPrefs.GetFloat("audioVolume", 0.5f);
        audioSource.Play();
    }

    // Update is called once per frame

}
