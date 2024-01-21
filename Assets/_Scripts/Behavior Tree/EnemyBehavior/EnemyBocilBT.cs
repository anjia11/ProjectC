using System;
using UnityEngine;

namespace _Scripts.Behavior_Tree.EnemyBehavior
{
    public class EnemyBocilBT: EnemyBT
    {
        protected override void Start()
        {
            base.Start();

            var selectorEnemy = new Selector("ENEMY SELECTOR");

            var attackSequence = new Sequence("Attack Sequence");

            var patrolSequence = new Sequence("Patrol Sequence");
            var patrol = new Leaf("Patrol", Patrol);
            var idleAnimPlay = new Leaf("Idle Play", Idle);

            var attack = new Sequence("Attack");
            var detectPlayer = new Leaf("Detect Player", DetectPlayer);
            var facePlayer = new Leaf("Face Player", FacePlayer);
            var attackPlayer = new Leaf("Attack Player", AttackPlayer);
            var waitAttack = new WaitNode(0.7f);
            var waitIdle = new WaitNode(2f);
            var waitBtAttack = new WaitNode(0.8f);
            var attackSelector = new RSelector("Random Attack Selector");
            var attack2 = new Sequence("Attack 2");
            var attackPlayer2 = new Leaf("Attack Player 2", AttackPlayer2);
            var chasingPlayer = new Leaf("Chasing Player", ChasingPlayer);
            var flip = new Leaf("Flip", Flipp);

            //invert detect
            var invertDetectPlayer = new Inverter("Invert Detect Player");
            invertDetectPlayer.AddChild(detectPlayer);
            
            patrolSequence.AddChild(idleAnimPlay);
            patrolSequence.AddChild(waitIdle);
            patrolSequence.AddChild(flip);
            patrolSequence.AddChild(patrol);
            
            attack.AddChild(facePlayer);
            attack.AddChild(attackPlayer);
            attack.AddChild(waitAttack);
            //
            attack2.AddChild(facePlayer);
            attack2.AddChild(attackPlayer2);
            attack2.AddChild(waitAttack);
            //
            
            attackSelector.AddChild(attack);
            attackSelector.AddChild(attack2);
            
            var cekMati = new Leaf("Cek Is Alive", CekMati);
            
            BehaviorTree whilePlayerNotDetected = new BehaviorTree();
            whilePlayerNotDetected.AddChild(invertDetectPlayer);
            Repeater rPlayerDetect = new Repeater("Repeat Until Player Detected", whilePlayerNotDetected);
            rPlayerDetect.AddChild(patrolSequence);

            attackSequence.AddChild(detectPlayer);
            attackSequence.AddChild(chasingPlayer);
            attackSequence.AddChild(attack2);
            attackSequence.AddChild(idleAnimPlay);
            attackSequence.AddChild(waitBtAttack);

            BehaviorTree whileNotDie = new BehaviorTree();
            whileNotDie.AddChild(cekMati);
            Repeater rIsMati = new Repeater("Repeat Until Die", whileNotDie);

            selectorEnemy.AddChild(attackSequence);
            selectorEnemy.AddChild(rPlayerDetect);
            rIsMati.AddChild(selectorEnemy);
            tree.AddChild(rIsMati);
            
            
            tree.PrintTree();
        }

        protected override void Update()
        {
            base.Update();
        }
        

        public NodeStatus ChasingPlayer()
        {
            Anim.Play("SL_Knight_Move");
            SetVelocity(5 * facingDirection, rb2D.velocity.y);
            
            if (IsPlayerDetected().distance < attackDistance && IsPlayerDetected())
                return NodeStatus.Success;
            
            if (!IsPlayerDetected())
                return NodeStatus.Failure;

            return NodeStatus.Running;
        }

        public NodeStatus AttackPlayer()
        {
            Anim.Play("SL_Knight_Attack");
            return NodeStatus.Success;
        }

        public NodeStatus AttackPlayer2()
        {
            Anim.Play("SL_Knight_Attack2");
            return NodeStatus.Success;
        }

        public NodeStatus Patrol()
        {
            if (CheckTouchingWall() || !IsGrounded())
            {
                return NodeStatus.Success;
            }
            Anim.Play("SL_Knight_Move");
            SetVelocity(3 * facingDirection, rb2D.velocity.y);
            return NodeStatus.Running;
        }

        public NodeStatus Idle()
        {
            Anim.Play("SL_Knight_Idle");
            return NodeStatus.Success;
        }
        
        
        #region Gizmoss

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        }

        public override void Die()
        {
            base.Die();
            Anim.Play("Mati");
            SetVelocity(0, rb2D.velocity.y);
            Destroy(gameObject, 3f);
        }

        #endregion
    }
}