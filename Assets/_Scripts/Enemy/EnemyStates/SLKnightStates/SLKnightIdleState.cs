using UnityEngine;

public class SLKnightIdleState : SLKnightGroundState
{
    public SLKnightIdleState(EnemyFSM enemyFsmBase, FiniteStateMachine stateMachine, string animBoolName, EnemyFsmSlKnight enemyFsm) : base(enemyFsmBase, stateMachine, animBoolName, enemyFsm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = EnemyFsm.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        EnemyFsm.SetZeroVelocity();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(EnemyFsm.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}