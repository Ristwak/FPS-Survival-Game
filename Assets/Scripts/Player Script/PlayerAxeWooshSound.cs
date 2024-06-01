using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeWooshSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] woosh_Sound;

    void Play_woosh_Sound()
    {
        audioSource.clip = woosh_Sound[Random.Range(0, woosh_Sound.Length)];

        audioSource.Play(); 
    }
}
