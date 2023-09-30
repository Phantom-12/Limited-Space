using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    public Vector2 RawMovementInput{get;private set;}
    public int NormInputX{get;private set;}
    public int NormInputY{get;private set;}
    public bool JumpInput{get;private set;}
    public bool JumpInputStop{get;private set;}

    [SerializeField]
    private float inputHoldTime;
    private float jumpStartTime;
    private float dashStartTime;

    private void Start()
    {
        playerInput=GetComponent<PlayerInput>();
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput=context.ReadValue<Vector2>();
        NormInputX=(int)(RawMovementInput.x*Vector2.right).normalized.x;
        NormInputY=(int)(RawMovementInput.y*Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
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
}
