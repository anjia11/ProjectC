public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    private bool isGrounded;
    public PlayerAbilityState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)
        {
            if (isGrounded && Player.CurrentVelocity.y < 0.01f)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else
            {
                StateMachine.ChangeState(Player.AirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Checks()
    {
        base.Checks();
        isGrounded = Player.IsGrounded();
    }
}