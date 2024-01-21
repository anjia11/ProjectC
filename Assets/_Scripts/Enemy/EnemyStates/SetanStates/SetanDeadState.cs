public class SetanDeadState :State
{
    private EnemyFsmSetan _enemyFsm;
    public SetanDeadState(EnemyFSM enemyFsmBase, FiniteStateMachine stateMachine, string animBoolName, EnemyFsmSetan enemyFsm) : base(enemyFsmBase, stateMachine, animBoolName)
    {
        this._enemyFsm = enemyFsm;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _enemyFsm.SetZeroVelocity();
    }
}