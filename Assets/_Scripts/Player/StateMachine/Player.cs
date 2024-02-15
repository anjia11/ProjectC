
using System.Collections;
using _Scripts.Entities;
using UnityEngine;

public class Player : Entity
{
    #region Komponen
    public PlayerInputHandler PlayerInputHandler { get; private set; }
    
    [SerializeField] private PlayerData playerData;

    #endregion

    #region Variabel lain
    public bool isBusy { get; private set; }
    public bool isDead;

    #endregion

    
    

    #region StateMachine
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }

    #endregion
    
   
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "InAir");
        AirState = new PlayerAirState(this, StateMachine, playerData, "InAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "Land");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "WallClimb");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "WallGrab");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "InAir");
        DashState = new PlayerDashState(this, StateMachine, playerData, "Dash");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, playerData, "PrimaryAttack");
        DeadState = new PlayerDeadState(this, StateMachine, playerData, "Die");
    }

    protected override void Start()
    {
        base.Start();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
        // facingDirection = 1;

        StateMachine.Initialize(IdleState);
    }
    
    protected override void Update()
    {
        base.Update();
        CurrentVelocity = rb2D.velocity;
        StateMachine.CurrentState.LogicUpdate();
        Dashing();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    
    
    

    #region Fungsi lain
    
    public void Dashing()
    {
        if (PlayerInputHandler.dashInput && SkillManager.instance._dashSkill.CanUseSkill() && !isDead)
        {
            StateMachine.ChangeState(DashState);
        }
        
        PlayerInputHandler.UseDashInput();
    }
    
    IEnumerator Jeda(float jedaDetik)
    {
        isBusy = true;
        yield return new WaitForSeconds(jedaDetik);
        isBusy = false;
    }
    
    #endregion

    #region Animasi

    void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    // public void AnimationActionTrigger()
    // {
    //     Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPointCollider.position, attackRadius, hitable);
    //     foreach (var item in enemies)
    //     {
    //         // IDamageable damageable = item.GetComponent<IDamageable>();
    //         // if (damageable != null)
    //         // {
    //         //     if (EnemyStats.instance.currentHealth > 0 )
    //         //         damageable.DoDamage(100);
    //         // }
    //         EnemyStats target = item.GetComponent<EnemyStats>();
    //         if (target != null)
    //         {
    //             CharacterStats.CalculateDamage(target);
    //         }
    //
    //         if (item.GetComponentInParent<MovingObject>()) 
    //             item.GetComponentInParent<MovingObject>().isDown = !item.GetComponentInParent<MovingObject>().isDown;
    //     }
    // }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override void Die()
    {
        base.Die();
        StateMachine.ChangeState(DeadState);
    }

    public void ComeToLive()
    {
        StateMachine.ChangeState(IdleState);
    }

    #endregion
    
    public bool cutscene = false;
    public IEnumerator WalkIntoNewScene(Vector2 exitDirection, float delay)
    {
        if (exitDirection.y > 0)
        {
            SetVelocityY(exitDirection.y * playerData.jumpForcce);
            SetVelocityX(exitDirection.x *2* playerData.moveVelocity);
        }
        
        if (exitDirection.x != 0)
        {
            //SetVelocityX(exitDirection.x * playerData.moveVelocity);
        }
        //Flip
        CheckFlip((int)exitDirection.x);
        yield return new WaitForSeconds(delay);
        cutscene = false;
    }

}
