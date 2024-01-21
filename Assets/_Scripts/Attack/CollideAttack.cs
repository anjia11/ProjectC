using UnityEngine;

namespace _Scripts.Attack
{
    public class CollideAttack : MonoBehaviour
    {
        [SerializeField] private int damage;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                IDamageable target = other.GetComponent<IDamageable>();
                if (target != null)
                {
                    target.DoDamage(damage);
                    Destroy(gameObject);
                }
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Destroy(gameObject);
            }
            
        }
    }
}