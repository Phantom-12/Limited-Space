using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool Pausing{get;private set;}
    [SerializeField]
    GameObject failedWindow,completeWindow,pauseWindow;
    // Start is called before the first frame update
    void Start()
    {
        failedWindow.SetActive(false);
        completeWindow.SetActive(false);
        pauseWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelComplete()
    {
        Pause(true);
        FindObjectOfType<Player>().Pause();
        completeWindow.SetActive(true);
    }

    public void LevelFailed()
    {
        Pause(true);
        FindObjectOfType<Player>().Pause();
        failedWindow.SetActive(true);
    }

    public void OnPauseButtonClick()
    {
        if(!Pausing)
        {
            OnPauseButtonClick(true);
        }
        else
        {
            OnPauseButtonClick(false);
        }
    }

    public void OnPauseButtonClick(bool state)
    {
        if(state)
        {
            Pause(true);
            pauseWindow.SetActive(true);
        }
        else
        {
            Pause(false);
            pauseWindow.SetActive(false);
        }
    }

    public void Pause()
    {
        if(!Pausing)
        {
            Pause(true);
        }
        else
        {
            Pause(false);
        }
    }

    public void Pause(bool state)
    {
        if(state)
        {
            Pausing=true;
            Time.timeScale=0;
        }
        else
        {
            Pausing=false;
            Time.timeScale=1;
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Pause(false);
    }

    public void ToNextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex+1>=SceneManager.sceneCount)
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Pause(false);
    }

    public void ToStartScene()
    {
        SceneManager.LoadScene(0);
        Pause(false);
    }
}
