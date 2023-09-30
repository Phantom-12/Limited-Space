using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int jumpTimesLeft;

    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        jumpTimesLeft=playerData.maxJumpTimes;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(playerData.jumpVelocity);
        DecreaseJumpTimesLeft();
        player.InAirState.SetIsJumping();
        isAbilityDone=true;
    }

    public bool CanJump()
    {
        return jumpTimesLeft>0;
    }

    public void ResetJumpTimesLeft()
    {
        jumpTimesLeft=playerData.maxJumpTimes;
    }

    public void DecreaseJumpTimesLeft()
    {
        jumpTimesLeft--;
    }
}
