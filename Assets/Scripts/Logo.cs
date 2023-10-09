using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip enterAudioClip;
    public AudioClip exitAudioClip;  
    bool play = false;
    float sTime = 0.5f;
    bool start = false;
    float eTime = 1f;
    bool end = false;
    float lastTime = 5.5f;

    void Update()
    {
        sTime -= Time.deltaTime;
        if (sTime < 0)
        {
            if(!start)
            {
                if (!play)
                {
                    audioSource.PlayOneShot(enterAudioClip, 0.5f);
                    play = true;
                    start = true;
                }

            }
            if (start)
            {
                lastTime -= Time.deltaTime;
                if (lastTime <= 0)
                {
                    audioSource.PlayOneShot(exitAudioClip, 0.5f);
                    eTime -= Time.deltaTime;
                    if(eTime <= 0)
                    {
                        end = true;
                    }
                }
            }
        }
        if (end)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
