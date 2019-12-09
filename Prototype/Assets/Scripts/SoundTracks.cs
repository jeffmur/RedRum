using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTracks : MonoBehaviour
{
    public AudioSource SoundTrack;
    public AudioClip BossMusic, IdleMusic, WaveMusic, Creepy;

    private void Start()
    {
        EventManager.Instance.onNewRoomEntered += IdleSound;
        EventManager.Instance.onRoomCompleted += Level1Sound;
        EventManager.Instance.onBossRoomEnter += BossSound;
        EventManager.Instance.onBossRoomCompleted += IdleSound;
        EventManager.Instance.onWaveStart += IdleSound;
        EventManager.Instance.onWaveEnd += Level2Sound;

        StartCoroutine(InitSound());
    }

    private IEnumerator InitSound()
    {
        SoundTrack.volume = 0;
        yield return new WaitForSeconds(1);
        SoundTrack.volume = 1;
    }

    private void IdleSound()
    {
        SoundTrack.clip = IdleMusic;
        SoundTrack.Play();
    }

    private void Level2Sound()
    {
        SoundTrack.clip = WaveMusic;
        SoundTrack.Play();
    }

    private void BossSound()
    {
        SoundTrack.clip = BossMusic;
        SoundTrack.Play();
    }

    private void Level1Sound()
    {
        SoundTrack.clip = Creepy;
        SoundTrack.Play();
    }
}
