using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimFinTeller : MonoBehaviour
{

    public void PlayerDie()
    {
        FindObjectOfType<SceneController>().LevelFailed();
    }

    public void PlayerSpwan()
    {
        FindObjectOfType<SceneController>().LevelBegin();
    }
    
    public void PlayerLeave()
    {
        FindObjectOfType<SceneController>().LevelCompleteWindow();
    }
}
