using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void DoDamage(int damage);
    void DoDamage(CharacterStats characterStats);
}
