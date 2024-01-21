using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Transform post1;
    public Transform post2;

    public Transform elev;

    public float speed;
    public bool isDown;

    public static MovingObject instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        StartMove();
    }

    void StartMove()
    {
        if (isDown)
        {
            elev.transform.position = Vector2.MoveTowards(elev.transform.position, post1.position, speed * Time.deltaTime);
        }
        else
        {
            elev.transform.position = Vector2.MoveTowards(elev.transform.position, post2.position, speed * Time.deltaTime);
        }
    }
}
