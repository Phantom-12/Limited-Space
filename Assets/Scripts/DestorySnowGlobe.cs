using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DestorySnowGlobe : MonoBehaviour
{
    Image SG;
    private void Start()
    {
        SG = GameObject.Find("SG").GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SG.color = new Color(255, 255, 255, 1);
            AudioSource audioPlayer = GameObject.Find("audioPlayer").GetComponent<AudioSource>();
            List<AudioClip> audioClipList = new List<AudioClip>();
            audioClipList.Add(Resources.Load<AudioClip>("Audio/主界面按钮"));
            audioClipList.Add(Resources.Load<AudioClip>("Audio/按钮声（除主界面外所有按钮全局触发）"));
            audioClipList.Add(Resources.Load<AudioClip>("Audio/电流（人物死亡时触发）"));
            audioClipList.Add(Resources.Load<AudioClip>("Audio/机器人（出场入场）"));
            audioClipList.Add(Resources.Load<AudioClip>("Audio/喷气背包（飞行&斜飞时触发）sfx"));
            audioClipList.Add(Resources.Load<AudioClip>("Audio/钥匙_final bpm 140"));
            audioClipList.Add(Resources.Load<AudioClip>("Audio/雪花球_final bpm 65"));
            audioPlayer.Pause();
            audioPlayer.clip = audioClipList[6];
            audioPlayer.time = 0.0f;
            audioPlayer.Play();
            Destroy(gameObject);
        }
    }
}
