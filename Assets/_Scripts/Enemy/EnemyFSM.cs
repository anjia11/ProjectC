using System.Collections;
using System.Collections.Generic;
using _Scripts.Entities;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyFSM : Enemy
{
    [Header("Move Atribut")]
    [SerializeField] public float idleTime = 1f;
    [SerializeField] public float chasingTime = 2f;
    
    [Header("Detect Player")]
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private Transform playerCheckPos;
    [SerializeField] private float distance;
    [SerializeField] private float areaDistance;
    [SerializeField] private Transform attackCheckPos;
    
    [Header("Attack Atribut")]
    [SerializeField] public float attackDistance;
    [SerializeField] public float attackColdown;
    [HideInInspector] public float lastTimeAttacked;
    
    public Transform playerTransform { get; private set; }
    
    public FiniteStateMachine StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new FiniteStateMachine();
    }

    protected override void Start()
    {
        base.Start();
        playerTransform = PlayerManager.instance._player.transform;
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.currentState.LogicUpdate();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheckPos.position, new Vector3(playerCheckPos.position.x + areaDistance, playerCheckPos.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(attackCheckPos.position, new Vector3(attackCheckPos.position.x + distance * facingDirection, attackCheckPos.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(attackCheckPos.position, new Vector3(attackCheckPos.position.x + attackDistance * facingDirection, attackCheckPos.position.y));
    }

    public void FixedUpdate()
    {
        StateMachine.currentState.PhysicsUpdate();
    }

    #region Check

    public RaycastHit2D IsPlayerInArea()
    {
        return Physics2D.Raycast(playerCheckPos.position, Vector2.right, areaDistance, playerLayerMask);
    }
    
    public RaycastHit2D IsPlayerDetected()
    {
        return Physics2D.Raycast(attackCheckPos.position, Vector2.right * facingDirection, distance, playerLayerMask);
    }

    #endregion

    #region Animasi

    void AnimationTrigger() => StateMachine.currentState.AnimationTrigger();
    void AnimationFinishTrigger() => StateMachine.currentState.AnimationFinishTrigger();

    #endregion
}
