using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private float dashCooldown;
    private bool isDashCooldown;
    public PlayerDashState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
    {
    }

    public override void Exit()
    {
        base.Exit();
        Player.SetZeroVelocity();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            Player.SetVelocity(PlayerData.dashVelocity * Player.facingDirection, 0f);
            if (Time.time >= startTime + PlayerData.dashTime)
            {
                isAbilityDone = true;
            }
            
        }
    }
}
