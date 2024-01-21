using UnityEngine;

public class SLKnightAttackState : State
{
    protected EnemyFsmSlKnight EnemyFsm;
    public SLKnightAttackState(EnemyFSM enemyFsmBase, FiniteStateMachine stateMachine, string animBoolName, EnemyFsmSlKnight enemyFsm) : base(enemyFsmBase, stateMachine, animBoolName)
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
        EnemyFsm.lastTimeAttacked = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        EnemyFsm.SetZeroVelocity();

        if (isAnimationFinish)
        {
            stateMachine.ChangeState(EnemyFsm.ChasingState);
        }
    }
    
    
}