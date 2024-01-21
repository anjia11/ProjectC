using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
    {
    }
}
