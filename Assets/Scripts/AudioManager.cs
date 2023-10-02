using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    PlayerInputHandler inputHandler;
    AudioSource audioSource;
    void Start()
    {
        inputHandler=FindObjectOfType<PlayerInputHandler>();
        audioSource=GetComponent<AudioSource>();
        StartCoroutine(nameof(Play));
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(inputHandler.SpaceHoldInput)
    //     {
    //         start=true;

    //     }
    // }

    IEnumerator Play()
    {
        while(!inputHandler.SpaceHoldInput)
            yield return null;
        audioSource.Play();
    }
}
