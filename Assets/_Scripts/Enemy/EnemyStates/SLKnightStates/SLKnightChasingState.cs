using UnityEngine;

public class SLKnightChasingState : State
{
    private int moveDir;
    protected EnemyFsmSlKnight EnemyFsm;
    public SLKnightChasingState(EnemyFSM enemyFsmBase, FiniteStateMachine stateMachine, string animBoolName, EnemyFsmSlKnight enemyFsm) : base(enemyFsmBase, stateMachine, animBoolName)
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
            stateTimer = EnemyFsm.chasingTime;
            
            if (EnemyFsm.IsPlayerDetected().distance < EnemyFsm.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(EnemyFsm.AttackState);
            }
        }
        else
        {
            if (stateTimer < 0)
                stateMachine.ChangeState(EnemyFsm.IdleState);
        }

        if (EnemyFsm.playerTransform != null)
        {
            if (EnemyFsm.playerTransform.position.x > EnemyFsm.transform.position.x)
                moveDir = 1;
            else if (EnemyFsm.playerTransform.position.x < EnemyFsm.transform.position.x)
                moveDir = -1;
        }
        
        EnemyFsm.CheckFlip(moveDir);
        
        EnemyFsm.SetVelocityX(5f * moveDir);
    }

    private bool CanAttack()
    {
        if (Time.time >= EnemyFsm.lastTimeAttacked + EnemyFsm.attackColdown)
        {
            EnemyFsm.lastTimeAttacked = Time.time;
            return true;
        }
        Debug.Log("Attack Cooldown");

        return false;
    }
}