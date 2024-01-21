using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.PlayerInputHandler.UseJumpInput();
        Player.JumpState.ResetJump();
        Player.SetVelocity(PlayerData.wallJumpVelocity, PlayerData.wallJumpAngle, wallJumpDirection);
        Player.CheckFlip(wallJumpDirection);
        Player.JumpState.DecreaseJump();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Player.Anim.SetFloat("yVelocity", Player.CurrentVelocity.y);
        Player.Anim.SetFloat("xVelocity", Mathf.Abs(Player.CurrentVelocity.x));

        if (Time.time >= startTime + PlayerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void TentukanArahWallJump(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -Player.facingDirection;
        }
        else
        {
            wallJumpDirection = Player.facingDirection;
        }
    }
}
