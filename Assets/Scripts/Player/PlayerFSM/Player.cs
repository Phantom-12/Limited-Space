using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Machine Variables 状态机变量
    public PlayerStateMachine StateMachine{get;private set;}

    public PlayerIdleState IdleState{get;private set;}
    public PlayerMoveState MoveState{get;private set;}
    public PlayerJumpState JumpState{get;private set;}
    public PlayerInAirState InAirState{get;private set;}

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components 组件
    public Animator Anim{get;private set;}
    public PlayerAudioController AudioController{get;private set;}
    public PlayerInputHandler InputHandler{get;private set;}
    public BoxCollider2D MovementCollider{get;private set;}
    public Rigidbody2D Rb{get;private set;}
    public Transform DashDirectionIndicator{get;private set;}
    #endregion

    #region Check Transforms 检测点
    [SerializeField]
    private Transform groundCheckPoint;
    #endregion

    #region Other Variables 其他变量
    public int FacingDirection{get;private set;}
    private Vector2 workspace;
    private bool pause=false;
    private bool win=false;
    #endregion

    #region Unity Callback Functions unity回调函数 
    private void Awake()
    {
        StateMachine=new PlayerStateMachine();

        IdleState=new PlayerIdleState(this,StateMachine,playerData,"idle");
        MoveState=new PlayerMoveState(this,StateMachine,playerData,"move");
        JumpState=new PlayerJumpState(this,StateMachine,playerData,"inAir");
        InAirState=new PlayerInAirState(this,StateMachine,playerData,"inAir");
        
        Anim=GetComponent<Animator>();
        AudioController=GetComponent<PlayerAudioController>();
        InputHandler=GetComponent<PlayerInputHandler>();
        Rb=GetComponent<Rigidbody2D>();
        MovementCollider=GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        DashDirectionIndicator=transform.Find("DashDirectionIndicator");

        FacingDirection=1;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        if(pause)
            return ;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        if(pause)
            return ;
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Funtions 设置函数
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity,Rb.velocity.y);
        Rb.velocity=workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(Rb.velocity.x,velocity);
        Rb.velocity=workspace;
    }

    public void SetVelocity(float velocity,Vector2 direction)
    {
        direction.Normalize();
        workspace.Set(direction.x*velocity,direction.y*velocity);
        Rb.velocity=workspace;
    }

    public void SetColliderHeight(float height)
    {
        Vector2 center=MovementCollider.offset;
        workspace.Set(MovementCollider.size.x,height);
        center.y+=(height-MovementCollider.size.y)/2;
        MovementCollider.size=workspace;
        MovementCollider.offset=center;
    }
    #endregion

    #region Check Funtions 检测函数
    public void FlipIfNeed(int xInput)
    {
        if(xInput!=0 && xInput!=FacingDirection)
            Flip();
    }

    public bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position,playerData.groundCheckRadius,playerData.whatIsGround);
    }
    #endregion

    #region Other Funtions 其他函数
    private void Flip()
    {
        FacingDirection*=-1;
        transform.Rotate(0.0f,180.0f,0.0f);
    }

    private void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    private void AnimationFinishedTrigger()
    {
        StateMachine.CurrentState.AnimationFinishedTrigger();
    }

    public void OnDrawGizmos()
    {
        //Debug.DrawRay(wallCheckPoint.position,Vector2.right*FacingDirection*playerData.wallCheckDistance,Color.blue);
    }

    public void Die()
    {
        if(!win){
            Pause();
            Rb.velocity=Vector2.zero;
            AudioController.PlayerDie();
            Anim.SetBool("die",true);
        }
    }

    public void Pause()
    {
        pause=true;
    }

    public void Spwan()
    {
        AudioController.PlayerSpwan();
        Anim.updateMode=AnimatorUpdateMode.UnscaledTime;
        Anim.SetTrigger("spwan");
    }

    public void SpwanEnd()
    {
        Anim.updateMode=AnimatorUpdateMode.Normal;
    }

    public void Leave()
    {
        AudioController.PlayerLeave();
        Anim.updateMode=AnimatorUpdateMode.UnscaledTime;
        Anim.SetTrigger("leave");
    }

    public void Win(){
        win=true;
    }
    #endregion
}
