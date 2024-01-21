using _Scripts.Entities;
using UnityEngine;

namespace _Scripts.Behavior_Tree.EnemyBehavior
{
    public class EnemyFlyerBT : Enemy
    {
        private BehaviorTree tree;
        private NodeStatus _status = NodeStatus.Running;
        [SerializeField] private GameObject ledakanEffect;
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            tree = new BehaviorTree();
            
            var sequencePatrol = new Sequence("Sequence Patrol");
            var patrol = new Leaf("Patrol", Patrol);
            var idleAnimPlay = new Leaf("Idle", Idle);
            var wait = new WaitNode(2);
            
            sequencePatrol.AddChild(patrol);
            sequencePatrol.AddChild(idleAnimPlay);
            sequencePatrol.AddChild(wait);
            
            tree.AddChild(sequencePatrol);
            tree.PrintTree();
        }

        protected override void Update()
        {
            base.Update();
            if (_status != NodeStatus.Success)
                tree.Process();
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        }

        public override void Die()
        {
            base.Die();
            Instantiate(ledakanEffect, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
        }

        public NodeStatus Patrol()
        {
            if (CheckTouchingWall())
            {
                Flip();
                return NodeStatus.Success;
            }
            Anim.Play("Lalat_Idle");
            SetVelocity(3 * facingDirection, rb2D.velocity.y);
            return NodeStatus.Running;
        }

        public NodeStatus Idle()
        {
            Anim.Play("Lalat_Idle");
            return NodeStatus.Success;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
                Die();
        }
    }
}