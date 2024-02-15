using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTriger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.EndPauseGame();
            Debug.Log("Game End");
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.EndPauseGame();
            Debug.Log("Game End");
            Destroy(gameObject);
        }
    }
}
