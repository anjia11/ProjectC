
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine StateMachine;
    protected Player Player;
    protected PlayerData PlayerData;
    private string animBoolName;
    protected bool isAnimationFinish;
    protected bool isExitingState;

    protected float startTime;
    protected float stateTimer;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName)
    {
        this.Player = player;
        this.StateMachine = playerStateMachine;
        this.PlayerData = playerData;
        this.animBoolName = animName;
    }

    public virtual void Enter()
    {
        Checks();
        Player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        //Debug.Log("Anim "+ animBoolName);
        isAnimationFinish = false;
        isExitingState = false;
    }
    
    public virtual void Exit()
    {
        Player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void PhysicsUpdate()
    {
        Checks();
    }

    public virtual void Checks()
    {
        
    }
    
    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinish = true;
}
