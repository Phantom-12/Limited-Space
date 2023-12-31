using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool Pausing{get;private set;}
    bool levelBegun=false;
    [SerializeField]
    GameObject failedWindow,completeWindow,pauseWindow;
    // Start is called before the first frame update
    void Start()
    {
        failedWindow.SetActive(false);
        completeWindow.SetActive(false);
        pauseWindow.SetActive(false);
        LevelBegin();
    }

    public void LevelBegin()
    {
        Pause(true);
        FindObjectOfType<Player>().Spwan();
    }

    public void LevelBeginEnd()
    {
        levelBegun=true;
        FindObjectOfType<Player>().SpwanEnd();
        Pause(false);
    }

    public void LevelComplete()
    {
        Pause(true);
        FindObjectOfType<Player>().Pause();
        FindObjectOfType<Player>().Leave();
    }

    public void LevelCompleteWindow()
    {
        Pause(true);
        completeWindow.SetActive(true);
        if (GameObject.FindWithTag("snowGlobe") == null)
        {
            GameObject.Find("Snowglobe").SetActive(true);
        }else{
            GameObject.Find("Snowglobe").SetActive(false);
        }
    }

    public void LevelFailed()
    {
        Pause(true);
        FindObjectOfType<Player>().Pause();
        failedWindow.SetActive(true);
        if (GameObject.FindWithTag("snowGlobe") == null)
        {
            GameObject.Find("snowglobe").SetActive(true);
        }else{
            GameObject.Find("snowglobe").SetActive(false);
        }
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
        if(!levelBegun)
            return;
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
        if(SceneManager.GetActiveScene().buildIndex+1>=SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("No Next Scene");
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Pause(false);
    }

    public void ToStartScene()
    {
        SceneManager.LoadScene("ChooseLevel");
        Pause(false);
    }

    public void ToDeathChoice()
    {
        SceneManager.LoadScene("DeathChoice");
        Pause(false);
    }

    public void ToLifeChoice()
    {
        SceneManager.LoadScene("LifeChoice");
        Pause(false);
    }
    public void ToDeafaltScene()
    {
        SceneManager.LoadScene("StartScene");
        Pause(false);
    }
}
