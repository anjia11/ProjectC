using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLKnightDeadState : State
{
    private EnemyFsmSlKnight _enemyFsm;
    public SLKnightDeadState(EnemyFSM enemyFsmBase, FiniteStateMachine stateMachine, string animBoolName, EnemyFsmSlKnight enemyFsm) : base(enemyFsmBase, stateMachine, animBoolName)
    {
        this._enemyFsm = enemyFsm;
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
        _enemyFsm.SetZeroVelocity();
    }
}
