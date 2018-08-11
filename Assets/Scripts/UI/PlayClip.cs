using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class PlayClip : MonoBehaviour 
{
    public AudioClip AlarmClip;
    public AudioClip BeginGameClip;
    public AudioClip StepClip;
    public AudioClip VaporisedClip;
    private AudioSource source;
    [SerializeField]
    private EventTrigger trigger;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Movement movement;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if(player)
            player.OnDie += PlayVaporisedClip;
        if(movement)
            movement.OnWalk += PlayStepClip;
    }

    private void PlayStepClip()
    {
        source.clip = StepClip;
        source.Play();
    }

    private void PlayVaporisedClip()
    {
        source.clip = VaporisedClip;
        source.Play();
    }

    public void PlayAlarm()
    {
        source.clip = AlarmClip;
        source.loop = true;
        source.Play();
    }

    public void PlayBeginGameClip()
    {
        source.clip = BeginGameClip;
        source.loop = false;
        source.Play();
    }


}
