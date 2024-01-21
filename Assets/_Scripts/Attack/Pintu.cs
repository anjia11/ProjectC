using UnityEngine;

public class Pintu: MonoBehaviour,IDamageable
{
    
    public int totalHealth = 3;
    public GameObject deathEffect;
    public void DoDamage(int damage)
    {
        totalHealth -= damage;
        if (totalHealth <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }

    public void DoDamage(CharacterStats characterStats)
    {
        
    }
}
