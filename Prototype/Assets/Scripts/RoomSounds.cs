using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSounds : MonoBehaviour
{
    public AudioSource gameSounds;
    public AudioClip waveStart, waveEnd, roomEntered,  roomExit, bossEnter, bossExit;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.onNewRoomEntered += RoomEnteredSound;
        EventManager.Instance.onRoomCompleted += RoomCompletedSound;
        EventManager.Instance.onBossRoomEnter += BossRoomEnterSound;
        EventManager.Instance.onBossRoomCompleted += BossDefeatedSound;
        EventManager.Instance.onWaveStart += WaveStart;
        EventManager.Instance.onWaveEnd += WaveEnd;
    }
    
    private void WaveStart()
    {
        //print("Wave Started");
        gameSounds.volume = 0.8f;
        gameSounds.clip = waveStart;
        gameSounds.Play();
    }

    private void WaveEnd()
    {
        //print("End of Wave");
        gameSounds.clip = waveEnd;
        gameSounds.Play();
    }

    private void RoomEnteredSound()
    {
        //print("New room entered");
        gameSounds.clip = roomEntered;
        gameSounds.Play();
    }

    private void RoomCompletedSound()
    { 
        //print("Room Completed");
        gameSounds.clip = roomExit;
        gameSounds.Play();
    }
    private void BossRoomEnterSound()
    {
        //print("BOSS ENGAGED");
        gameSounds.clip = bossEnter;
        gameSounds.Play();
    }
    private void BossDefeatedSound()
    {
        //print("BOSS DEAD");
        gameSounds.clip = bossExit;
        gameSounds.Play();
    }

    /** Jayden's Needy Needs
     * NEW SOUNDTRACK FOR EACH BOSS
     * 
     */
}
