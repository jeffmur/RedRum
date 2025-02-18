﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : SceneSingleton<RoomHandler>
{
    [SerializeField]
    public delegate void RoomEventDelegate();
    public event RoomEventDelegate onNewWave, onWaveComplete, onNewRoomEnter, onRoomCompleted, onBossRoomEnter, onBossDefeated;
    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        RoomManager[] allRooms = FindObjectsOfType<RoomManager>();

        // Check for Level2 - Puzzle
        SpawnWaves p = FindObjectOfType<SpawnWaves>();
        if (p) { SubscribeToEvents(p); }

        // Otherwise, iterate through S1, S2, W, BR
        foreach (RoomManager r in allRooms)
            SubscribeToEvents(r);
    }

    private void SubscribeToEvents(RoomManager room)
    {
        room.onNewRoomEnter += TriggerRoomEnter;
        room.onRoomCompleted += TriggerRoomCompleted;
        room.onBossRoomEnter += TriggerBossEnter;
        room.onBossDefeated += TriggerBossDefeated;
    }
    private void SubscribeToEvents(SpawnWaves room)
    {
        room.onNewWave += TriggerWaveStart;
        room.onWaveComplete += TriggerWaveEnd;
        room.onBossRoomEnter += TriggerBossEnter;
        room.onBossDefeated += TriggerBossDefeated;
    }
    // ---------- LEVEL 1 ------------
    public void TriggerRoomEnter()
    {
        onNewRoomEnter?.Invoke();
    }

    public void TriggerRoomCompleted()
    {
        onRoomCompleted?.Invoke();
    }

    // ---------- LEVEL 2 ------------
    public void TriggerWaveStart()
    {
        onNewWave?.Invoke();
    }

    public void TriggerWaveEnd()
    {
        onWaveComplete?.Invoke();
    }

    // ---------- BOTH ------------
    public void TriggerBossEnter()
    {
        onBossRoomEnter?.Invoke();
    }

    public void TriggerBossDefeated()
    {
        onBossDefeated?.Invoke();
    }
}
