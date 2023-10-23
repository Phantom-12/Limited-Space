using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    public Vector2 RawMovementInput{get;private set;}
    public Vector2 MovementInput{get;private set;}
    public int NormInputX{get;private set;}
    public int NormInputY{get;private set;}
    public bool JumpInput{get;private set;}
    public bool JumpInputStop{get;private set;}
    public bool SpaceHoldInput{get;private set;}
    public bool SpaceDoubleTapInput{get;private set;}

    [SerializeField]
    private float inputHoldTime;
    private float jumpStartTime;

    SceneController sceneController;


    private void Start()
    {
        playerInput=GetComponent<PlayerInput>();
        sceneController=FindObjectOfType<SceneController>();
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(sceneController.Pausing)
            return;
        RawMovementInput=context.ReadValue<Vector2>();
        MovementInput=RawMovementInput.normalized;
        NormInputX=(int)(RawMovementInput.x*Vector2.right).normalized.x;
        NormInputY=(int)(RawMovementInput.y*Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(sceneController.Pausing)
            return;
        if(context.performed)
        {
            JumpInput=true;
            JumpInputStop=false;
            jumpStartTime=Time.time;
        }
        else if(context.canceled)
        {
            JumpInputStop=true;
        }
    }
    public void OnSpaceHoldInput(InputAction.CallbackContext context)
    {
        if(sceneController.Pausing)
            return;
        if(context.performed)
        {
            // Debug.Log("长按");
            SpaceHoldInput=true;
        }
        else if(context.canceled)
        {
            // Debug.Log("长按cancel");
            SpaceHoldInput=false;
        }
    }

    public void OnSpaceDown()
    {
        SpaceHoldInput=true;
    }

    public void OnSpaceUp()
    {
        SpaceHoldInput=false;
    }

    public void OnSpaceHoldDev(bool state)
    {
        if(sceneController.Pausing)
            return;
        if(state)
        {
            // Debug.Log("长按");
            SpaceHoldInput=true;
        }
        else if(!state)
        {
            // Debug.Log("长按cancel");
            SpaceHoldInput=false;
        }
    }

    public void OnSpaceDoubleTapInput(InputAction.CallbackContext context)
    {
        if(sceneController.Pausing)
            return;
        if(context.performed)
        {
            // Debug.Log("双击");
            SpaceDoubleTapInput=true;
        }
    }

    public void OnMoveInput(Vector2 Input)
    {
        if(sceneController.Pausing)
            return;
        RawMovementInput=Input;
        MovementInput=RawMovementInput.normalized;
        NormInputX=(int)(RawMovementInput.x*Vector2.right).normalized.x;
        NormInputY=(int)(RawMovementInput.y*Vector2.up).normalized.y;
    }

    public void OnJumpInput()
    {
        if(sceneController.Pausing)
            return;
        JumpInput=true;
        JumpInputStop=false;
        jumpStartTime=Time.time;
        
        JumpInputStop=true;
    }

    public void UseJumpInput()
    {
        JumpInput=false;
    }

    public void UseJumpInputStop()
    {
        JumpInputStop=false;
    }

    private void CheckJumpInputHoldTime()
    {
        if(Time.time>jumpStartTime+inputHoldTime)
        {
            JumpInput=false;
        }
    }

    public void UseSpaceDoubleTapInput()
    {
        SpaceDoubleTapInput=false;
    }
}
