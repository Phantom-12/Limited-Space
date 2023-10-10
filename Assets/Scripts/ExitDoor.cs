using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField]
    Key key;

    bool hasKey=false;

    Animator animator;

    void Awake()
    {
        animator=GetComponent<Animator>();
    }

    public void GetKey()
    {
        hasKey=true;
        animator.SetBool("hasKey",true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasKey)
        {
            FindObjectOfType<SceneController>().LevelComplete();
        }
    }
}
