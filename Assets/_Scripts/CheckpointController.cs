using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    private Animator anim;
    public string checkPointId;
    public bool activated;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    [ContextMenu("Generate id")]
    public void GenerateID()
    {
        checkPointId = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            ActivateCheckPoint();
        }
    }

    public void ActivateCheckPoint()
    {
        RespawnController.instance.SetSpawn(transform.position);
        anim.SetBool("active", true);
        activated = true;
        Debug.Log("Active");
    }
}