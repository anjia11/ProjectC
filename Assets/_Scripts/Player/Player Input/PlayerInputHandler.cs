using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 rawMoveInput { get; private set; }
    public int normalizeInputX { get; private set; }
    public int normalizeInputY { get; private set; }
    public bool isJumpInput { get; private set; }
    public bool jumpInputCancel;

    public bool dashInput { get; private set; }
    public bool attackInput { get; private set; }
    
    
    [SerializeField] private float inputHolDTime = 0.2f;
    private float jumpInputStartTime;


    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        rawMoveInput = context.ReadValue<Vector2>();
        if (Mathf.Abs(rawMoveInput.x) > 0.5f)
        {
            normalizeInputX = (int)(rawMoveInput * Vector2.right).normalized.x;
        }else
        {
            normalizeInputX = 0;
        }
        
        normalizeInputY = (int)(rawMoveInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isJumpInput = true;
            jumpInputCancel = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            jumpInputCancel = true;
        }
    }

    public void UseJumpInput() => isJumpInput = false;

    void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHolDTime)
        {
            isJumpInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dashInput = true;
        }
    }

    public void UseDashInput() => dashInput = false;

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackInput = true;
        }
    }

    public void UseAttackInput() => attackInput = false;
}
