using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    [SerializeField]
    private float videoLength;
    private void Start()
    {
        StartCoroutine(DelayFunc());
        //Time.timeScale = 0;
        //Debug.Log("000");
    }
    IEnumerator DelayFunc()
    {
        yield return new WaitForSeconds(videoLength);
        //Debug.Log("111");
        GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("StartScene");
    }
}
