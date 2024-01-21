using System.Collections;
using UnityEngine;

namespace _Scripts.Behavior_Tree.EnemyBehavior
{
    public class MiniBossBT : EnemyBT
    {
        [SerializeField] private Transform areaBoss;
        [SerializeField] private Vector2 areaWidth;

        [SerializeField] private Transform[] fireBallPos;
        [SerializeField] private GameObject fireBall;
        
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            
            var detectPlayer = new Leaf("Detect Player", PlayerInArea);
            var facePlayer = new Leaf("Face Player", FacePlayer);
            var attackPlayer = new Leaf("Attack Player", AttackPlayer);
            var waitAttack = new WaitNode(0.7f);
            var waitBtAttack = new WaitNode(1f);
            var attackPlayer2 = new Leaf("Attack Player 2", AttackPlayer2);
            var attackPlayer3 = new Leaf("Attack Player 3", AttackPlayer3);
            var fireBall = new Leaf("Fire Ball", FireBallAttack);
            
            var idle = new Leaf("Idle", Idle);
            var patrol = new Leaf("Patrol", Patrol);
            var wait = new WaitNode(1f);

            var chasingPlayer = new Leaf("Chasing", ChasingPlayer);

            
            var selectorMiniBoss = new Selector("Selector Mini Boss");
            var sequencePatrol = new Sequence("Sequence Patrol");
            var sequenceAttack = new Sequence("Sequence Attack");
            var sequenceComboAttack = new Sequence("Attack");
            
            var flip = new Leaf("Flip", Flipp);
            
            var invertDetectPlayer = new Inverter("Invert Detect Player");
            invertDetectPlayer.AddChild(detectPlayer);
            
            
            sequenceComboAttack.AddChild(facePlayer);
            sequenceComboAttack.AddChild(idle);
            sequenceComboAttack.AddChild(waitBtAttack);
            sequenceComboAttack.AddChild(attackPlayer);
            sequenceComboAttack.AddChild(waitAttack);
            sequenceComboAttack.AddChild(attackPlayer2);
            sequenceComboAttack.AddChild(waitAttack);
            sequenceComboAttack.AddChild(attackPlayer3);
            sequenceComboAttack.AddChild(waitAttack);
            sequenceComboAttack.AddChild(fireBall);
            
            sequenceAttack.AddChild(detectPlayer);
            sequenceAttack.AddChild(chasingPlayer);
            sequenceAttack.AddChild(sequenceComboAttack);
            
            BehaviorTree whilePlayerNotDetected = new BehaviorTree();
            whilePlayerNotDetected.AddChild(invertDetectPlayer);
            Repeater rPlayerDetect = new Repeater("Repeat Until Player Detected", whilePlayerNotDetected);
            
            
            sequencePatrol.AddChild(patrol);
            sequencePatrol.AddChild(flip);
            sequencePatrol.AddChild(idle);
            sequencePatrol.AddChild(wait);
            
            rPlayerDetect.AddChild(sequencePatrol);
            
            selectorMiniBoss.AddChild(sequenceAttack);
            selectorMiniBoss.AddChild(rPlayerDetect);
            
            var cekMati = new Leaf("Cek Is Alive", CekMati);
            BehaviorTree whileNotDie = new BehaviorTree();
            whileNotDie.AddChild(cekMati);
            Repeater rIsMati = new Repeater("Repeat Until Die", whileNotDie);

            rIsMati.AddChild(selectorMiniBoss);
            tree.AddChild(rIsMati);
            
            tree.PrintTree();
        }

        protected override void Update()
        {
            base.Update();
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(areaBoss.position, areaWidth);
        }

        public override void Die()
        {
            base.Die();
            Anim.Play("Mati");
            SetVelocity(0, rb2D.velocity.y);
            Destroy(transform.parent.gameObject, 3f);
        }

        NodeStatus AttackPlayer()
        {
            Anim.Play("W_Attack1");
            return NodeStatus.Success;
        }

        NodeStatus FireBallAttack()
        {
            foreach (var firePos in fireBallPos)
            {
                GameObject fireAttack = Instantiate(fireBall, firePos.position, Quaternion.identity);
                fireAttack.transform.SetParent(transform.parent);
            }
            return NodeStatus.Success;
        }
        
        NodeStatus AttackPlayer2()
        {
            Anim.Play("W_Attack2");
            return NodeStatus.Success;
        }
        
        NodeStatus AttackPlayer3()
        {
            Anim.Play("W_Attack1");
            return NodeStatus.Success;
        }
        
        NodeStatus Idle()
        {
            Anim.Play("W_Idle");
            SetVelocity(0, rb2D.velocity.y);
            return NodeStatus.Success;
        }

        bool IsPlayerInArea()
        {
            return Physics2D.BoxCast(areaBoss.position, areaWidth, 0, Vector2.zero, distance, hitable);
        }
        

        NodeStatus PlayerInArea()
        {
            if (IsPlayerInArea())
            {
                return NodeStatus.Success;
            }
            return NodeStatus.Failure;
        }

        NodeStatus ChasingPlayer()
        {
            SetVelocityX(3 * facingDirection);
            Anim.Play("W_Idle");
            
            if (PlayerInArea() == NodeStatus.Success && IsPlayerInAreaAttack())
                return NodeStatus.Success;{}
            

            if (!IsPlayerInArea())
                return NodeStatus.Failure;
            
            FacePlayer();
            if (FacePlayer() == NodeStatus.Success)
                return NodeStatus.Running;

            return NodeStatus.Running;

        }
        
        NodeStatus Patrol()
        {
            if (CheckTouchingWall() || !IsGrounded())
            {
                return NodeStatus.Success;
            }
            SetVelocity(1 * facingDirection, rb2D.velocity.y);
            Anim.Play("W_Idle");
            return NodeStatus.Running;
        }
    }
}