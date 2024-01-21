using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{
    public bool UnlockDash, UnlockDobelJump, UnlockBecomeBall, UnlockDropBomb;
    public GameObject OrbEffect;
    public string unlockMessage;
    public TMP_Text unlockText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

            if (UnlockDash)
            {
                player.canDashing = true;
            }

            if (UnlockBecomeBall)
            {
                player.canBecomeBall = true;
            }

            if (UnlockDobelJump)
            {
                player.canDobelJump = true;
            }

            if (UnlockDropBomb)
            {
                player.canDropBomb = true;
            }

            Instantiate(OrbEffect, transform.position, transform.rotation);
            //membuat child gameobject menjadi parent
            unlockText.transform.parent.SetParent(null);
            //membuat gameobject sama posisi dengan parent sebelumnya.
            unlockText.transform.parent.position = transform.position;
            //atur pesan keluar
            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);

            Destroy(unlockText.transform.parent.gameObject, 5.0f);
            Destroy(gameObject, 0.1f);
        }
    }
}