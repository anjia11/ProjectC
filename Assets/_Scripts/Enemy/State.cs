using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected EnemyFSM EnemyFsmBase;
    protected float startTime;
    protected string animBoolName;
    protected bool isExitingState;
    protected float stateTimer;
    protected bool isAnimationFinish;

    public State(EnemyFSM enemyFsmBase, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.EnemyFsmBase = enemyFsmBase;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        EnemyFsmBase.Anim.SetBool(animBoolName, true);
        isExitingState = false;
        isAnimationFinish = false;
    }

    public virtual void Exit()
    {
        EnemyFsmBase.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void PhysicsUpdate()
    {
        
    }
    
    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinish = true;
}
