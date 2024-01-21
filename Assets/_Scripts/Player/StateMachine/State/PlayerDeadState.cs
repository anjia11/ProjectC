using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.isDead = true;
        
        stateTimer = 1;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Player.SetZeroVelocity();

        // if (!isExitingState)
        // {
        //     
        // }
        if (isAnimationFinish)
        {
            RespawnController.instance.Respawn();
            Player.isDead = false;
            //StateMachine.ChangeState(Player.IdleState);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
