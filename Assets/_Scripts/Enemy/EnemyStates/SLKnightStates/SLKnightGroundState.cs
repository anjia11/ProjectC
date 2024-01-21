using UnityEngine;

public class SLKnightGroundState : State
{
    protected EnemyFsmSlKnight EnemyFsm;
    public SLKnightGroundState(EnemyFSM enemyFsmBase, FiniteStateMachine stateMachine, string animBoolName, EnemyFsmSlKnight enemyFsm) : base(enemyFsmBase, stateMachine, animBoolName)
    {
        this.EnemyFsm = enemyFsm;
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
        if (EnemyFsm.IsPlayerInArea() && EnemyFsm.IsPlayerDetected())
        {
            stateMachine.ChangeState(EnemyFsm.ChasingState);
        }
    }
}