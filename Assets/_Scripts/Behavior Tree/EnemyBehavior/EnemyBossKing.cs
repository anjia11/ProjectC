using UnityEngine;

namespace _Scripts.Behavior_Tree.EnemyBehavior
{
    public class EnemyBossKing : EnemyBT
    {
        [Header("Trigger")]
        [SerializeField] private GameObject endGameTriggerPrefab;
        private GameObject tempatTrigger;
        
        [Header("Variabel BT")]
        [SerializeField] float blinkDistance = 2f;
        [SerializeField] private Transform posKanan;
        [SerializeField] private Transform posKiri;

        [SerializeField] private GameObject petirVFXPrefab;
        [SerializeField] private Transform petirTransform;

        [SerializeField] private Transform blinkTransform;
        [SerializeField] private GameObject smokeVFXPrefab;
        [SerializeField] private Transform[] smokeSpawnTransform;
        [SerializeField] private Transform[] petirWavePos;
        
        [SerializeField] private GameObject dashFX;
        [SerializeField] private Transform dashPos;
        
        
        private float timeCool;
        private float seconds = 10f;

        protected override void Awake()
        {
            base.Awake();
            tempatTrigger = GameObject.FindGameObjectWithTag("Trigger");
        }

        protected override void Start()
        {
            base.Start();
            timeCool = seconds;
            
            RSelector randomMeleeSelectorBoss = new RSelector("Random Melee Boss Selector");
            Sequence idleSequence = new Sequence("Idle Sequence");
            var sequenceBoss = new Sequence("Boss Sequence");
            var randomAttackSelector = new RSelector("Random Attack Selector");
            var comboMeleeAttack = new Sequence("Melee Attack Sequence");
            var rangeSequence = new Sequence("Range Attack Sequence");
            var meleeSequence = new Sequence("Melee Sequence");
            var randomRangeAttackSelector = new RSelector("Random Range Attack Selector");
            var thunderAttackSequence = new Sequence("Thunder Attack");
            var dashAttackSequence = new Sequence("Dash Attack");
            var airAttack = new Sequence("Air Attack");

            //cek
            var checkDistanceDekat = new Leaf("Check Player Dekat", CheckDistance);
            var facePlayer = new Leaf("Face Player", FacePlayer);
            Inverter invertCheckDistance = new Inverter("Check Distance");
            invertCheckDistance.AddChild(checkDistanceDekat);
            // wait
            var waitAttack = new WaitNode(0.4f);
            var waitBtAttack = new WaitNode(1f);
            var waitBfAttack = new WaitNode(0.2f);

            // idle
            Leaf idle = new Leaf("Idle", Idle);

            idleSequence.AddChild(idle);
            idleSequence.AddChild(waitBtAttack);

            //melee attack
            Leaf attackPlayer = new Leaf("Attack 1", AttackPlayer);
            Leaf attackPlayer2 = new Leaf("Attack 2", AttackPlayer2);
            Leaf attackPlayer3 = new Leaf("Attack 3", AttackPlayer3);

            // sequence melee attack
            comboMeleeAttack.AddChild(facePlayer);
            comboMeleeAttack.AddChild(attackPlayer);
            comboMeleeAttack.AddChild(waitAttack);
            comboMeleeAttack.AddChild(attackPlayer2);
            comboMeleeAttack.AddChild(waitAttack);
            comboMeleeAttack.AddChild(attackPlayer3);
            comboMeleeAttack.AddChild(waitAttack);

            // blink attack
            Leaf blink = new Leaf("Blink", BlinkAttack);
            Leaf dashStance = new Leaf("Play Dash Anim", PlayDashAnim);
            Leaf endDashAnim = new Leaf("End Dash", PlayEndDashAnim);

            // sequence dash attack
            dashAttackSequence.AddChild(facePlayer);
            dashAttackSequence.AddChild(idle);
            dashAttackSequence.AddChild(waitBfAttack);
            dashAttackSequence.AddChild(dashStance);
            dashAttackSequence.AddChild(waitAttack);
            dashAttackSequence.AddChild(blink);
            dashAttackSequence.AddChild(endDashAnim);
            dashAttackSequence.AddChild(waitAttack);

            //Petir Attack
            Leaf castPetir = new Leaf("Play Cast Petir Anim", PlayPetirCasting);
            Leaf playpetirVFX = new Leaf("Play Petir VFX", PlayPetirVFX);
            
            thunderAttackSequence.AddChild(facePlayer);
            thunderAttackSequence.AddChild(idle);
            thunderAttackSequence.AddChild(waitBfAttack);
            thunderAttackSequence.AddChild(castPetir);
            thunderAttackSequence.AddChild(waitBfAttack);
            thunderAttackSequence.AddChild(playpetirVFX);
            thunderAttackSequence.AddChild(waitBfAttack);
            
            //Air Attack
            Leaf playLandingAnim = new Leaf("Play Landing Anim", PlayLandingAnim);
            Leaf playFallAnim = new Leaf("Play Fall Anim", PlayFallAnim);
            Leaf playSmokeFX = new Leaf("Play Smoke FX", PlaySmokeVFX);
            Leaf startBlinkAirAttack = new Leaf("Blink Air Attack", BlinkAirAttack);
            Leaf zeroGravity = new Leaf("Tahan", ZeroGravity);
            Leaf cooldown = new Leaf("Cooldown", Cooldown);
            Leaf petirWave = new Leaf("Petir Wave", PetirWave);

            airAttack.AddChild(cooldown);
            airAttack.AddChild(facePlayer);
            airAttack.AddChild(dashStance);
            airAttack.AddChild(waitBtAttack);
            airAttack.AddChild(startBlinkAirAttack);
            airAttack.AddChild(zeroGravity);
            airAttack.AddChild(waitAttack);
            airAttack.AddChild(playFallAnim);
            airAttack.AddChild(playLandingAnim);
            airAttack.AddChild(playSmokeFX);
            airAttack.AddChild(waitBtAttack);
            airAttack.AddChild(petirWave);
            airAttack.AddChild(waitBtAttack);
            
            // Random Range Attack
            randomRangeAttackSelector.AddChild(dashAttackSequence);
            randomRangeAttackSelector.AddChild(thunderAttackSequence);
            
            rangeSequence.AddChild(invertCheckDistance);
            rangeSequence.AddChild(randomRangeAttackSelector);
            
            //Random Melee Attack
            randomMeleeSelectorBoss.AddChild(comboMeleeAttack);
            randomMeleeSelectorBoss.AddChild(dashAttackSequence);
            
            meleeSequence.AddChild(checkDistanceDekat);
            meleeSequence.AddChild(randomMeleeSelectorBoss);
            
            randomAttackSelector.AddChild(rangeSequence);
            randomAttackSelector.AddChild(meleeSequence);
            randomAttackSelector.AddChild(airAttack);

            sequenceBoss.AddChild(randomAttackSelector);
            sequenceBoss.AddChild(idleSequence);
            
            var cekMati = new Leaf("Cek Is Alive", CekMati);
            BehaviorTree whileNotDie = new BehaviorTree();
            whileNotDie.AddChild(cekMati);
            Repeater rIsMati = new Repeater("Repeat Until Die", whileNotDie);

            rIsMati.AddChild(sequenceBoss);
            
            tree.AddChild(rIsMati);
            tree.PrintTree();

        }

        protected override void Update()
        {
            base.Update();
            if (timeCool > 0)
            {
                timeCool -= Time.deltaTime;
                if (timeCool <= 0)
                {
                    timeCool = 0;
                }

                //Debug.Log("COOLDOWN == " + timeCool);
            }
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        }

        public override void Die()
        {
            base.Die();
            Anim.Play("Mati");
            GameObject o = Instantiate(endGameTriggerPrefab, endGameTriggerPrefab.transform.position,
                Quaternion.identity);
            o.transform.SetParent(tempatTrigger.transform);
            SetVelocity(0, rb2D.velocity.y);
            Destroy(transform.parent.gameObject, 3f);
        }

        NodeStatus CheckDistance()
        {
            if (IsPlayerInAreaAttack())
            {
                return NodeStatus.Success;
            }

            return NodeStatus.Failure;
        }

        public NodeStatus AttackPlayer()
        {
            Anim.Play("Attack1");
            return NodeStatus.Success;
        }

        public NodeStatus AttackPlayer2()
        {
            Anim.Play("Attack2");
            return NodeStatus.Success;
        }

        public NodeStatus AttackPlayer3()
        {
            Anim.Play("Attack3");
            return NodeStatus.Success;
        }

        public NodeStatus Idle()
        {
            Anim.Play("Idle");
            SetVelocity(0, rb2D.velocity.y);
            return NodeStatus.Success;
        }

        NodeStatus BlinkAttack()
        {
            StartBlink();
            return NodeStatus.Success;
        }

        // void BlinkTowardsPlayer()
        // {
        //     Vector2 direction = (PlayerManager.instance._player.transform.position - transform.position).normalized;
        //     //Debug.Log("Direction == " + direction);
        //     // Implementasikan blink ke arah pemain
        //
        //     StartBlink();
        // }

        void StartBlink()
        {
            Vector2 newPosition = new Vector2(transform.position.x + facingDirection * blinkDistance,
                transform.position.y);

            if (CheckTouchingWall())
            {
                if (facingDirection == 1)
                {
                    transform.position = posKanan.position;
                }

                if (facingDirection == -1)
                {
                    transform.position = posKiri.position;
                }
            }
            else
            {
                // Pindahkan musuh ke posisi baru
                transform.position = newPosition;
            }
        }

        NodeStatus PlayDashAnim()
        {
            //SetZeroVelocity();
            Anim.Play("Dash");
            return NodeStatus.Success;
        }

        NodeStatus PlayEndDashAnim()
        {
            GameObject df = Instantiate(dashFX, dashPos.position, Quaternion.identity);
            df.transform.localScale = new Vector3(facingDirection, 1, 1);
            df.transform.SetParent(transform);
            Anim.Play("EndDash");
            return NodeStatus.Success;
        }

        NodeStatus PlayPetirVFX()
        {
            GameObject petir = Instantiate(petirVFXPrefab, petirTransform.position, Quaternion.identity);
            petir.transform.localScale = new Vector3(facingDirection, 1, 1);
            petir.transform.SetParent(transform);
            return NodeStatus.Success;
        }

        NodeStatus PlayPetirCasting()
        {
            Anim.Play("Casting");
            SetZeroVelocity();
            return NodeStatus.Success;
        }

        NodeStatus PlayFallAnim()
        {
            Anim.Play("Fall");
            rb2D.gravityScale = 40;
            if (IsGrounded())
                return NodeStatus.Success;

            return NodeStatus.Running;
        }

        NodeStatus PlayLandingAnim()
        {
            Anim.Play("Landing");
            return NodeStatus.Success;
        }
        
        NodeStatus PlaySmokeVFX()
        {
            GameObject k = Instantiate(smokeVFXPrefab, smokeSpawnTransform[0].position, Quaternion.identity);
            GameObject k2 = Instantiate(smokeVFXPrefab, smokeSpawnTransform[1].position, Quaternion.identity);

            if (facingDirection == 1)
            {
                k2.transform.localScale = new Vector3(-1, 1, 1);
                k2.transform.SetParent(transform);
                k.transform.SetParent(transform);
            }else if (facingDirection == -1)
            {
                k.transform.localScale = new Vector3(-1, 1, 1);
                k.transform.SetParent(transform);
                k2.transform.SetParent(transform);
            }
            return NodeStatus.Success;
        }
        
        void StartBlinkAirAttack()
        {
            Vector2 newPosition = blinkTransform.position;
            // Pindahkan musuh ke posisi baru
            transform.position = newPosition;
        }

        NodeStatus BlinkAirAttack()
        {
            StartBlinkAirAttack();
            return NodeStatus.Success;
        }

        NodeStatus ZeroGravity()
        {
            rb2D.gravityScale = 0;
            SetZeroVelocity();
            return NodeStatus.Success;
        }

        
        NodeStatus Cooldown()
        {
            if (timeCool <= 0)
            {
                timeCool = seconds;
                return NodeStatus.Success;
            }
            
            return NodeStatus.Failure;
        }

        NodeStatus PetirWave()
        {
            foreach (var pos in petirWavePos)
            {
                GameObject fireAttack = Instantiate(petirVFXPrefab, pos.position, Quaternion.identity);
                fireAttack.transform.Rotate(new Vector3(0,0,90));
                fireAttack.transform.SetParent(transform);
            }
            return NodeStatus.Success;
        }
    }
}