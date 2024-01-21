using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetanIdleState : State
{
    private EnemyFsmSetan _enemyFsmSetan;
    public SetanIdleState(EnemyFSM enemyFsmBase, FiniteStateMachine stateMachine, string animBoolName, EnemyFsmSetan enemyFsmSetan) : base(enemyFsmBase, stateMachine, animBoolName)
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
    }
}
