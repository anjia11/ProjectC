using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchWallState
{
    public PlayerWallGrabState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
    {
    }
}
