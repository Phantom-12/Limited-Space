using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private bool isGrounded;
    private bool isTouchingWall;

    public PlayerGroundedState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
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
        player.JumpState.ResetJumpTimesLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!hasExited)
        {
            if(player.InputHandler.JumpInput && player.JumpState.CanJump() && isGrounded)
            {
                player.InputHandler.UseJumpInput();
                stateMachine.ChangeState(player.JumpState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
