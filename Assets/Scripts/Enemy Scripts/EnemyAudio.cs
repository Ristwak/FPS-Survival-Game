using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip scream_clip, die_clip;

    [SerializeField]
    private AudioClip[] attack_clip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void play_screamSound()
    {
        audioSource.clip = scream_clip;
        audioSource.Play();
    }

    public void play_attackSound()
    {
        audioSource.clip = attack_clip[Random.Range(0,attack_clip.Length)];
        audioSource.Play();
    }

    public void play_dieSound()
    {
        audioSource.clip = die_clip;
        audioSource.Play();
    }
}
