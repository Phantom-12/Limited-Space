using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool hasExited;
    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player,PlayerStateMachine playerStateMachine,PlayerData playerData,string animBoolName)
    {
        this.player=player;
        this.stateMachine=playerStateMachine;
        this.playerData=playerData;
        this.animBoolName=animBoolName;
    }

    public virtual void Enter()
    {
        Dochecks();
        player.Anim.SetBool(animBoolName,true);
        startTime=Time.time;
        isAnimationFinished=false;
        hasExited=false;
        //Debug.Log(animBoolName);
    }

    public virtual void Exit()
    {
        hasExited=true;
        player.Anim.SetBool(animBoolName,false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        Dochecks();
    }

    public virtual void Dochecks()
    {

    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishedTrigger()
    {
        isAnimationFinished=true;
    }
}
