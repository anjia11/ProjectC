using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLKnightMoveState : SLKnightGroundState
{
    public SLKnightMoveState(EnemyFSM enemyFsmBase, FiniteStateMachine stateMachine, string animBoolName, EnemyFsmSlKnight enemyFsm) : base(enemyFsmBase, stateMachine, animBoolName, enemyFsm)
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
        EnemyFsm.SetVelocityX(2f * EnemyFsm.facingDirection);
        if (EnemyFsm.CheckTouchingWall() || !EnemyFsm.IsGrounded())
        {
            stateMachine.ChangeState(EnemyFsm.IdleState);
            EnemyFsm.Flip();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
