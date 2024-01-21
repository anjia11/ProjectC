using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int sisaJump;
    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
    {
        sisaJump = playerData.jumlahJump;
    }

    public override void Enter()
    {
        base.Enter();
        Player.PlayerInputHandler.UseJumpInput();
        Player.SetVelocityY(PlayerData.jumpForcce);
        isAbilityDone = true;
        sisaJump--;
        Player.AirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (sisaJump > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetJump() => sisaJump = PlayerData.jumlahJump;

    public void DecreaseJump() => sisaJump--;
}
