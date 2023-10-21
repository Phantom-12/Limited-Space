using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    AudioSource audioPlayer;
    List<AudioClip> audioClipList;
    bool flying=false;

    void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        audioClipList = new List<AudioClip>
        {
            Resources.Load<AudioClip>("Audio/电流（人物死亡时触发）"),
            Resources.Load<AudioClip>("Audio/机器人（出场入场）"),
            Resources.Load<AudioClip>("Audio/喷气背包改"),
        };
    }

    void Update()
    {
        if(flying && audioPlayer.time>2.0416f)
        {
            audioPlayer.time=1.75f;
        }
    }

    public void PlayerSpwan()
    {
        audioPlayer.Pause();
        audioPlayer.clip = audioClipList[1];
        audioPlayer.time = 0.0f;
        audioPlayer.Play();
    }

    public void PlayerLeave()
    {
        audioPlayer.Pause();
        audioPlayer.clip = audioClipList[1];
        audioPlayer.time = 0.0f;
        audioPlayer.Play();
    }

    public void PlayerDie()
    {
        audioPlayer.Pause();
        audioPlayer.clip = audioClipList[0];
        audioPlayer.time = 0.0f;
        audioPlayer.Play();
    }

    public void PlayerFlyStart()
    {
        if(flying)
            return;
        flying=true;
        audioPlayer.Pause();
        audioPlayer.clip = audioClipList[2];
        audioPlayer.time = 0.0f;
        audioPlayer.Play();
    }

    public void PlayerFlyEnd()
    {
        // Debug.Log("!");
        flying=false;
        audioPlayer.time = 2.0416f;
    }
}
