using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource footstep_sound;

    [SerializeField]
    private AudioClip[] footstep_Clip;

    private CharacterController character_Controller;

    [HideInInspector]
    public float volume_Min,volume_Max;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distance;

    void Awake()
    {
        footstep_sound = GetComponent<AudioSource>();

        character_Controller = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        if(!character_Controller.isGrounded)
            return;
        
        if(character_Controller.velocity.sqrMagnitude > 0)
        {
            //  Accumulated distance is the value of how far we will we go after wich the sound plays
            accumulated_Distance += Time.deltaTime;

            if(accumulated_Distance > step_Distance)
            {
                footstep_sound.volume = Random.Range(volume_Min,  volume_Max);
                footstep_sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_sound.Play();

                accumulated_Distance = 0f;
            }
        }
        else
        {
            accumulated_Distance = 0f;
        }
    }
}
