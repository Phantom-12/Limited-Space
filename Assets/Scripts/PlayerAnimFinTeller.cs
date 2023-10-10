using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimFinTeller : MonoBehaviour
{

    public void PlayerDieAnimFin()
    {
        FindObjectOfType<SceneController>().LevelFailed();
    }

    public void PlayerSpwanAnimFin()
    {
        FindObjectOfType<SceneController>().LevelBeginEnd();
    }
    
    public void PlayerLeaveAnimFin()
    {
        FindObjectOfType<SceneController>().LevelCompleteWindow();
    }
}
