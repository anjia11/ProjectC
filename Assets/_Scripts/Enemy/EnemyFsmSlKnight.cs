using UnityEngine;

public class EnemyFsmSlKnight : EnemyFSM
{
    #region States

    public SLKnightIdleState IdleState { get; private set; }
    public SLKnightMoveState MoveState { get; private set; }
    public SLKnightChasingState ChasingState { get; private set; }
    public SLKnightAttackState AttackState { get; private set; }
    public SLKnightDeadState DeadState { get; private set; }

    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        IdleState = new SLKnightIdleState(this, StateMachine, "Idle", this);
        MoveState = new SLKnightMoveState(this, StateMachine, "Move", this);
        ChasingState = new SLKnightChasingState(this, StateMachine, "Move", this);
        AttackState = new SLKnightAttackState(this, StateMachine, "Attack", this);
        DeadState = new SLKnightDeadState(this, StateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start();
        //facingDirection = 1;
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        StateMachine.ChangeState(DeadState);
        Destroy(gameObject, 3);
    }
}