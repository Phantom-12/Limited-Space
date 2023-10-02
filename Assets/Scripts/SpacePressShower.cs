using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpacePressShower : MonoBehaviour
{
    PlayerInputHandler inputHandler;
    Image image;
    [SerializeField]
    Sprite spaceUp,spaceDown;
    // Start is called before the first frame update
    void Awake()
    {
        inputHandler=FindObjectOfType<PlayerInputHandler>();
        image=GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputHandler.SpaceHoldInput)
            image.sprite=spaceUp;
        else
            image.sprite=spaceDown;
    }
}
