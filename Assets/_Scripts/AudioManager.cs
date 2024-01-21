using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] soundEffect;
    void PlaySoundEffect(int index)
    {
        soundEffect[index].Play();
    }
}
