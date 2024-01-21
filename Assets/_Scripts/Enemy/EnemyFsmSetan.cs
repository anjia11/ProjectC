using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFsmSetan : EnemyFSM
{
    #region States

    public SetanIdleState IdleState { get; private set; }
    public SetanDeadState DeadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        IdleState = new SetanIdleState(this, StateMachine, "Idle", this);
        DeadState = new SetanDeadState(this, StateMachine, "Die", this);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Die()
    {
        base.Die();
        StateMachine.ChangeState(DeadState);
    }
}
