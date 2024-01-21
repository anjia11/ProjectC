using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchWallState : PlayerState
{
    protected bool isTouchingWall;
    protected bool isGrounded;
    protected int inputX;
    protected bool jumpInput;
    public PlayerTouchWallState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
    {
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

        inputX = Player.PlayerInputHandler.normalizeInputX;
        jumpInput = Player.PlayerInputHandler.isJumpInput;
        if (jumpInput)
        {
            // Player.PlayerInputHandler.UseJumpInput();
            Player.WallJumpState.TentukanArahWallJump(isTouchingWall);
            Player.StateMachine.ChangeState(Player.WallJumpState);
        }
        else if (isGrounded)
        {
            StateMachine.ChangeState(Player.IdleState);
        }else if (!isTouchingWall || inputX != Player.facingDirection)
        {
            StateMachine.ChangeState(Player.AirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Checks()
    {
        base.Checks();
        isGrounded = Player.IsGrounded();
        isTouchingWall = Player.CheckTouchingWall();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
