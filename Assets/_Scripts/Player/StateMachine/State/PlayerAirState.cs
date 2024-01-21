using UnityEngine;

public class PlayerAirState : PlayerState
{
    private int inputX;
    private bool isGrounded;
    private bool jumpInput;
    private bool coyoteTime;
    private bool isJumping;
    private bool jumpInputCancel;
    private bool isTouchingWallBelakang;
    private bool attackInput;

    private bool isTouchingWall;
    private bool isDashCooldown;
    
    public PlayerAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animName) : base(player, playerStateMachine, playerData, animName)
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
        CheckCoyoteTime();
        
        inputX = Player.PlayerInputHandler.normalizeInputX;
        jumpInput = Player.PlayerInputHandler.isJumpInput;
        jumpInputCancel = Player.PlayerInputHandler.jumpInputCancel;
        attackInput = Player.PlayerInputHandler.attackInput;

        JumpMultiplier();
        
        if (isGrounded && Player.CurrentVelocity.y < 0.01f)
        {
            Player.PlayerInputHandler.UseAttackInput();
            StateMachine.ChangeState(Player.LandState);
        }
        // else if (attackInput)
        // {
        //     Player.PlayerInputHandler.UseAttackInput();
        //     StateMachine.ChangeState(Player.PrimaryAttackState);
        // }
        else if (jumpInput && (isTouchingWall || isTouchingWallBelakang))
        {
            isTouchingWall = Player.CheckTouchingWall();
            Player.WallJumpState.TentukanArahWallJump(isTouchingWall);
            StateMachine.ChangeState(Player.WallJumpState);
        }
        else if (jumpInput && Player.JumpState.CanJump())
        { ;
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (isTouchingWall && inputX == Player.facingDirection && Player.CurrentVelocity.y <= 0)
        {
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else
        {
            Player.CheckFlip(inputX);
            Player.SetVelocityX(PlayerData.moveVelocity * inputX);
            Player.Anim.SetFloat("yVelocity", Player.CurrentVelocity.y);
            Player.Anim.SetFloat("xVelocity", Mathf.Abs(Player.CurrentVelocity.x));
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
        isTouchingWall = Player.CheckTouchingWall();
        isTouchingWallBelakang = Player.CheckTouchingWallBelakang();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + PlayerData.coyoteTime)
        {
            coyoteTime = false;
            Player.JumpState.DecreaseJump();
        }
    }

    public void StartCoyoteTime()
    {
        coyoteTime = true;
    }

    private void JumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputCancel)
            {
                Player.SetVelocityY(Player.CurrentVelocity.y * PlayerData.jumpHeightMultiplier);
                isJumping = false;
            }
            else if (Player.CurrentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }

    public void SetIsJumping() => isJumping = true;
}