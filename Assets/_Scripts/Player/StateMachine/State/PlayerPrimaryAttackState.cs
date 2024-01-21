using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerPrimaryAttackState : PlayerAbilityState
{
    private int comboCounter;
    private int inputX;
    private float lastTimeAttacked;
    public PlayerPrimaryAttackState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        inputX = Player.PlayerInputHandler.normalizeInputX;
        Player.PlayerInputHandler.UseAttackInput();
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + PlayerData.comboResetTime)
        {
            comboCounter = 0;
        }
        
        Player.Anim.SetInteger("ComboCounter", comboCounter);
        
        float attackDirection = Player.facingDirection;
        if (inputX != 0)
        {
            attackDirection = inputX;
        }
        Player.SetVelocity(PlayerData.attackMovement[comboCounter].x * attackDirection, PlayerData.attackMovement[comboCounter].y);
        
        stateTimer = .1f;
        //isAbilityDone = true;
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer < 0)
        {
            Player.SetZeroVelocity();
        }

        if (!isExitingState)
        {
            if (isAnimationFinish)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }
    }
}
