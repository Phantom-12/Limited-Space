using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MovetoScence : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GameObject.Find("Canvas").GetComponent<AudioSource>();
    }
    public void ChangeScene1()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void ChangeScene2()
    {
        SceneManager.LoadScene("Scene2");
    }
    public void ChangeScene3()
    {
        SceneManager.LoadScene("Scene3");
    }
    public void ChangeScene4()
    {
        SceneManager.LoadScene("Scene4");
    }
    public void ChangeScene5()
    {
        SceneManager.LoadScene("Scene5");
    }
    public void ChangeScene6()
    {
        SceneManager.LoadScene("Scene6");
    }
    public void ChangeScene7()
    {
        SceneManager.LoadScene("Scene7");
    }
    public void ChangeScene8()
    {
        SceneManager.LoadScene("Scene8");
    }
    public void back()
    {
        PlayerPrefs.SetFloat("audioTime", audioSource.time);
        SceneManager.LoadScene("StartScene");
    }
}
