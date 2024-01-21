
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{

    private bool isGrounded;
    public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
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
        Player.CheckFlip(inputX);
        Player.SetVelocityX(PlayerData.moveVelocity * inputX);
        if (!isExitingState)
        {
            if (inputX == 0)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else if (!isGrounded)
            {
                StateMachine.ChangeState(Player.AirState);
            }
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
    }
}
