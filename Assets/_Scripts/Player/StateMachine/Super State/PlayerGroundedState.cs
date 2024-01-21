using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{

    protected int inputX;
    private bool jumpInput;
    private bool attackInput;
    private bool isGrounded;
    private bool isDashCooldown;
    public PlayerGroundedState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.JumpState.ResetJump();
        isGrounded = Player.IsGrounded();
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
        attackInput = Player.PlayerInputHandler.attackInput;

        if (jumpInput && Player.JumpState.CanJump())
        {
            // Player.PlayerInputHandler.UseJumpInput();
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!isGrounded)
        {
            Player.AirState.StartCoyoteTime();
            StateMachine.ChangeState(Player.AirState);
        }
        else if (attackInput && isGrounded)
        {
            Player.PlayerInputHandler.UseAttackInput();
            StateMachine.ChangeState(Player.PrimaryAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Checks()
    {
        base.Checks();
        //isDashCooldown = Player.CheckCooldown();
    }
}
