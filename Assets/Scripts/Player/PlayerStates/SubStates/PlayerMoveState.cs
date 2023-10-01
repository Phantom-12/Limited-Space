using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void Dochecks()
    {
        base.Dochecks();
    }

    public override void Enter()
    {
        base.Enter();
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
            player.FlipIfNeed(player.InputHandler.NormInputX);
            if(player.InputHandler.NormInputX==0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(Mathf.Abs(player.InputHandler.NormInputY)>=1e-5)
            player.SetVelocity(playerData.movementVelocity,player.InputHandler.MovementInput);
        else
            player.SetVelocityX(playerData.movementVelocity*player.InputHandler.NormInputX);
    }
}
