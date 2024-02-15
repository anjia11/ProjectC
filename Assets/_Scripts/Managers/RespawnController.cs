using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;
    public Vector3 respawnPoint;
    public float waitToRespawn;
    GameObject player;

    private void Awake()
    {
        if (instance != null)
        {
            //Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerStats.instance.gameObject;
        respawnPoint = player.transform.position;
    }

    public void Respawn()
    {
        StartCoroutine(WaitBackToLive());
    }

    IEnumerator WaitBackToLive()
    {
        UIController.instance.StartfadeToBlack();
        player.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        GameManager.Instance.RestartGame();
        UIController.instance.StartFadeFromBlack();
        player.transform.position = respawnPoint;
        player.SetActive(true);
        player.GetComponent<Player>().ComeToLive();
    }

    public Vector3 SetSpawn(Vector3 newCheckPoint)
    {
        return respawnPoint = newCheckPoint;
    }
}