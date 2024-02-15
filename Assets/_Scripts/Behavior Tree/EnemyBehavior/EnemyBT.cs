using System;
using System.Collections;
using _Scripts.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Behavior_Tree.EnemyBehavior
{
    public class EnemyBT : Enemy
    {
        // public enum State
        // {
        //     Idle, Working
        // }
        protected int moveDir;

        
        //[SerializeField]private State state = State.Working;

        private NodeStatus _status = NodeStatus.Running;
        
        protected BehaviorTree tree;
        
        float actionTimer = 0f;
        private float startTime;
        
        [Header("Detect Player")]
        [SerializeField] protected LayerMask playerLayerMask;
        [SerializeField] protected float distance;
        [SerializeField] protected Transform attackCheckPos;
        
        [Header("Attack Atribut")]
        [SerializeField] public float attackDistance;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            tree = new BehaviorTree();
        }

        protected override void Update()
        {
            base.Update();
            if (_status == NodeStatus.Running)
                tree.Process();
        }

        IEnumerator Behave()
        {
            if (_status != NodeStatus.Success)
            {
                tree.Process();
            }

            yield return new WaitForSeconds(Random.Range(0.0f, 0.1f));
        }
        
        public NodeStatus Wait(float seconds)
        {
            if (actionTimer < 0)
            {
                 actionTimer = seconds;
                 return NodeStatus.Success;
            }
            
            actionTimer -= Time.deltaTime;
            //Debug.Log("Action Timer = " + actionTimer);
            return NodeStatus.Running;
        
        }

        // public NodeStatus Wait(float waitTime)
        // {
        //     if (_status == NodeStatus.Running)
        //     {
        //         float elapsedTime = Time.time - startTime;
        //         if (elapsedTime >= waitTime)
        //         {
        //             _status = NodeStatus.Success;
        //         }
        //         Debug.Log("Wait == " + elapsedTime);
        //     }else if (_status == NodeStatus.Failure)
        //     {
        //         _status = NodeStatus.Running;
        //     }
        //     else
        //     {
        //         _status = NodeStatus.Running;
        //         startTime = Time.time;
        //     }
        //     return _status;
        // }

        public RaycastHit2D IsPlayerDetected()
        {
            return Physics2D.Raycast(attackCheckPos.position, Vector2.right * facingDirection, distance, playerLayerMask);
        }

        public NodeStatus DetectPlayer()
        {
            if (IsPlayerDetected())
            {
                return NodeStatus.Success;
            }
            else
            {
                return NodeStatus.Failure;
            }
        }
        
        protected NodeStatus FacePlayer()
        {
            if (PlayerManager.instance._player.transform.position.x > transform.position.x)
            {
                moveDir = 1;
            }else if (PlayerManager.instance._player.transform.position.x < transform.position.x)
            {
                moveDir = -1;
            }
            CheckFlip(moveDir);

            return NodeStatus.Success;
        }
        
        public NodeStatus Flipp()
        {
            Flip();
            return NodeStatus.Success;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.green;
            Gizmos.DrawLine(attackCheckPos.position, new Vector3(attackCheckPos.position.x + distance * facingDirection, attackCheckPos.position.y));
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(attackCheckPos.position, new Vector3(attackCheckPos.position.x + attackDistance * facingDirection, attackCheckPos.position.y));
        }

        #region Cek

        protected bool IsPlayerInAreaAttack()
        {
            var distanceToPlayer = Vector3.Distance(PlayerManager.instance._player.transform.position, transform.position);
            if (distanceToPlayer <= attackDistance)
            {
                return true;
            }

            return false;
        }

        #endregion
        
        protected NodeStatus CekMati()
        {
            if(CharacterStats.currentHealth <= 0) 
            {
                return NodeStatus.Failure;
            }
            else
            {
                return NodeStatus.Success;
            }
        }
        
        protected void MatiAnim()
        {
          
            Anim.Play("Mati");
            SetVelocity(0, rb2D.velocity.y);
            Destroy(gameObject, 3f);
        }
    }
}