using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GameObject.Find("Canvas").GetComponent<AudioSource>();
    }
    public void start()
    {
        PlayerPrefs.SetFloat("audioTime", audioSource.time);
        SceneManager.LoadScene("ChooseLevel");
    }
    public void exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//编辑状态下退出
#else
        Application.Quit();//打包编译后退出
#endif
    }
}
