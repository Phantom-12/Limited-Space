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
        SceneManager.LoadScene("Level1");
    }
    public void ChangeScene2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void ChangeScene3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void ChangeScene4()
    {
        SceneManager.LoadScene("Level+3");
    }
    public void ChangeScene5()
    {
        SceneManager.LoadScene("Level4");
    }
    public void ChangeScene6()
    {
        SceneManager.LoadScene("Level+4");
    }
    public void ChangeScene7()
    {
        SceneManager.LoadScene("Level5");
    }
    public void ChangeScene8()
    {
        SceneManager.LoadScene("Level+2");
    }
    public void ChangeScene9()
    {
        SceneManager.LoadScene("Level6");
    }
    public void ChangeScene10()
    {
        SceneManager.LoadScene("Level+1");
    }
    public void ChangeScene11()
    {
        SceneManager.LoadScene("Level7");
    }
    public void ChangeScene12()
    {
        SceneManager.LoadScene("Level8");
    }
    public void back()
    {
        PlayerPrefs.SetFloat("audioTime", audioSource.time);
        SceneManager.LoadScene("StartScene");
    }
}
