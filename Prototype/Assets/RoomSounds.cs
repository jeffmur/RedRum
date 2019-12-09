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
        print("Wave Started");
    }

    private void WaveEnd()
    {
        print("End of Wave");
    }

    private void RoomEnteredSound()
    {
        print("New room entered");
    }

    private void RoomCompletedSound()
    {
        print("Room Completed");
    }
    private void BossRoomEnterSound()
    {
        print("BOSS ENGAGED");
    }
    private void BossDefeatedSound()
    {
        print("YEET");
    }
}
