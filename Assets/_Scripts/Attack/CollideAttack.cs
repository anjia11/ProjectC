using _Scripts.Entities;
using UnityEngine;

namespace _Scripts.Attack
{
    public class CollideAttack : MonoBehaviour
    {
        private int moveDir;
        [SerializeField] private int damage;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.transform.position.x > transform.parent.position.x)
                {
                    moveDir = 1;
                }else if (other.transform.position.x < transform.parent.position.x)
                {
                    moveDir = -1;
                }
                IDamageable target = other.GetComponent<IDamageable>();
                if (target != null)
                {
                    target.DoDamage(damage);
                    other.GetComponent<Entity>().DamageFX(moveDir);
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