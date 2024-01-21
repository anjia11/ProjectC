using _Scripts.Entities;

public class EnemyStats : CharacterStats
{
    private Enemy _enemy;
    private ItemDrop _itemDrop;
    
    protected override void Start()
    {
        base.Start();
        _enemy = GetComponent<Enemy>();
        _itemDrop = GetComponent<ItemDrop>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        _enemy.DamageFX();
    }

    protected override void Die()
    {
        base.Die();
        _enemy.Die();
        _itemDrop.RandomDrop();
    }
}