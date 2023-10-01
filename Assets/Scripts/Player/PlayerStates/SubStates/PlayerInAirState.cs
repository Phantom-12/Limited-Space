using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private bool isTouchingWall,isTouchingWallBack,isTouchingLedge;
    private int xInput,yInput;
    private bool jumpInput,jumpInputStop;
    private bool grabInput;
    private bool dashInput;
    private bool isJumping;
    
    private bool coyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void Dochecks()
    {
        base.Dochecks();
        isGrounded=player.CheckGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        isGrounded=player.CheckGrounded();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Dochecks();
        if(!hasExited)
        {
            xInput=player.InputHandler.NormInputX;
            yInput=player.InputHandler.NormInputY;
            jumpInput=player.InputHandler.JumpInput;
            jumpInputStop=player.InputHandler.JumpInputStop;
            CheckCoyoteTime();
            //CheckJumpMultiplier();
            if(isGrounded && player.Rb.velocity.y<=0.03f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            if(jumpInput && player.JumpState.CanJump())
            {
                coyoteTime=false;
                player.InputHandler.UseJumpInput();
                stateMachine.ChangeState(player.JumpState);
            }
            else
            {
                player.FlipIfNeed(xInput);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        xInput=player.InputHandler.NormInputX;
        if(xInput!=0)
            player.SetVelocityX(playerData.movementVelocity*xInput);
        player.Anim.SetFloat("xVelocity",Mathf.Abs(player.Rb.velocity.x));
        player.Anim.SetFloat("yVelocity",player.Rb.velocity.y);
    }

    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time>startTime+playerData.coyoteTime)
        {
            coyoteTime=false;
            player.JumpState.DecreaseJumpTimesLeft();
        }
    }

    private void CheckJumpMultiplier()
    {
        if(isJumping)
        {
            if(player.Rb.velocity.y<=0.01f)
            {
                isJumping=false;
            }
            else if(jumpInputStop)
            {
                player.InputHandler.UseJumpInputStop();
                player.SetVelocityY(player.Rb.velocity.y*playerData.variableJumpHeightMultiplier);
            }
        }
    }

    public void StartCoyoteTime()
    {
        coyoteTime=true;
    }

    public void SetIsJumping()
    {
        isJumping=true;
    }
}
