using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField]
    bool FinalLevel;
    [SerializeField]
    bool lifeOrDeath;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            collider.gameObject.GetComponent<Player>().Win();
            Invoke("Win",0.5f);
        }
    }
    void Win(){
        if(FinalLevel){
            if(lifeOrDeath){
                GameObject.Find("SceneController").GetComponent<SceneController>().ToLifeChoice();
            }else{
                GameObject.Find("SceneController").GetComponent<SceneController>().ToDeathChoice();
            }
        }else{
            GameObject.Find("SceneController").GetComponent<SceneController>().LevelComplete();
        }
    }
}
