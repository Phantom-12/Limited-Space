using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieTeller : MonoBehaviour
{

    public void PlayerDie()
    {
        FindObjectOfType<SceneController>().LevelFailed();
    }
}
