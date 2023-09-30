using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerDate",menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity=10f;

    [Header("Jump State")]
    public float jumpVelocity=15f;
    public int maxJumpTimes=2;

    [Header("In Air State")]
    public float coyoteTime=0.2f;
    public float variableJumpHeightMultiplier=0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity=-3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity=3f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity=20;
    public float wallJumpTime=0.4f;
    public Vector2 wallJumpAngle=new Vector2(1,2);

    [Header("Ledge Climb State")]
    public Vector2 startOffest;
    public Vector2 stopOffest;

    [Header("Dash State")]
    public float dashCooldown=0.5f;
    public float maxDashHoldTime=1f;
    public float dashTimeScale=0.25f;
    public float dashTime=0.2f;
    public float dashVeclocity=30f;
    public float drag=10f;
    public float dashEndYMutiplier=0.2f;
    public float distanceBetweenAfterImages=0.5f;

    [Header("Crough State")]
    public float croughMovementVelocity=5f;
    public float crouchColliderHeight=0.8f;
    public float standColliderHeight=1.6f;

    [Header("Check Variables")]
    public float groundCheckRadius=0.3f;
    public float wallCheckDistance=0.5f;
    public LayerMask whatIsGround;

}
