using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenButton : MonoBehaviour
{
    PlayerInputHandler inputHandler;
    void Awake()
    {
        inputHandler=FindObjectOfType<PlayerInputHandler>();
    }

    public void ButtonDown()
    {
        inputHandler.OnSpaceDown();
    }

    public void ButtonUp()
    {
        inputHandler.OnSpaceUp();
    }
}
