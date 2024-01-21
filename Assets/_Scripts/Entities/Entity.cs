using System.Collections;
using UnityEngine;

namespace _Scripts.Entities
{
    public class Entity : MonoBehaviour
    {
        #region Komponen

        public Rigidbody2D rb2D { get; private set; }
        public Animator Anim { get; private set; }
        public EntityFX entityFX { get; private set; }
        public CharacterStats CharacterStats { get; private set; }

        #endregion
    
        #region Check Variabels

        [Header("Check Atribut")]
        [SerializeField] protected Transform groundCheck;
        [SerializeField] protected Transform wallCheck;
        [SerializeField] protected float groundCheckRadius = 0.3f;
        [SerializeField] protected LayerMask groundLayer;
        [SerializeField] protected float wallCheckDistance = 0.3f;

        #endregion
    
        #region Attack
        [Header("Attack Info")]
        [SerializeField] protected LayerMask hitable;

        #endregion

        #region KnockBack
        [Header("KnockBack")]
        [SerializeField] protected Vector2 knockBackDirection;
        [SerializeField] protected float duration = 0.1f;
        protected bool isKnocked;

        #endregion

        #region Variabel Lain

        [HideInInspector]public Vector2 CurrentVelocity;
        private Vector2 workspace;
        [SerializeField]public int facingDirection;

        #endregion
    
    
        protected virtual void Awake()
        {
        
        }

        protected virtual void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            Anim = GetComponentInChildren<Animator>();
            entityFX = GetComponent<EntityFX>();
            CharacterStats = GetComponent<CharacterStats>();
        }

        protected virtual void Update()
        {
        
        }
    
        #region Set Velocity Function

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            if (isKnocked)
                return;
        
            angle.Normalize();
            workspace.Set(angle.x * velocity * direction, angle.y * velocity);
            rb2D.velocity = workspace;
            CurrentVelocity = workspace;
        }
    
        public void SetVelocity(float xVelocity, float yVelovity)
        {
            if (isKnocked)
                return;
        
            workspace.Set(xVelocity, yVelovity);
            rb2D.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocityX(float velocity)
        {
            if (isKnocked)
                return;
        
            workspace.Set(velocity, CurrentVelocity.y);
            rb2D.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocityY(float velocity)
        {
            if (isKnocked)
                return;
        
            workspace.Set(CurrentVelocity.x, velocity);
            rb2D.velocity = workspace;
            CurrentVelocity = workspace;
        }
    
        public void SetZeroVelocity()
        {
            if (isKnocked)
                return;
        
            workspace.Set(0, 0);
            rb2D.velocity = workspace;
            CurrentVelocity = workspace;
        }
        public void SetZeroVelocityc()
        {
            workspace.Set(0, 0);
            rb2D.velocity = workspace;
            CurrentVelocity = workspace;
        }

        #endregion
    
        #region CheckFunction

        public virtual bool IsGrounded() =>
            Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        public virtual bool CheckTouchingWall() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection,
            wallCheckDistance, groundLayer);
    
        public virtual bool CheckTouchingWallBelakang() => Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDirection,
            wallCheckDistance, groundLayer);
    
        public virtual void CheckFlip(int inputX)
        {
            if (inputX != 0 && inputX != facingDirection)
            {
                Flip();
            }
        }
    

        #endregion

        #region Gizmoss

        public virtual void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
        }

        #endregion


        #region Fungsi Lain

        public virtual void Flip()
        {
            facingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }

        public void DamageFX()
        {
            if (gameObject.activeSelf)
            {
                entityFX.StartCoroutine("KedipFX");
                StartCoroutine(KnockBack());
            }
        }

        public IEnumerator KnockBack()
        {
            isKnocked = true;
            rb2D.velocity = new Vector2(knockBackDirection.x * -facingDirection, knockBackDirection.y);

            yield return new WaitForSeconds(duration);
            SetZeroVelocityc();
            isKnocked = false;
        }

        public virtual void Die()
        {
        
        }

        #endregion

    
    }
}
