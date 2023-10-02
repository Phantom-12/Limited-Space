using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    GameObject failedWindow,completeWindow;
    // Start is called before the first frame update
    void Start()
    {
        failedWindow.SetActive(false);
        completeWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelComplete()
    {
        FindObjectOfType<Player>().Pause();
        Time.timeScale=0;
        completeWindow.SetActive(true);
    }

    public void LevelFailed()
    {
        FindObjectOfType<Player>().Pause();
        Time.timeScale=0;
        failedWindow.SetActive(true);
    }
}
