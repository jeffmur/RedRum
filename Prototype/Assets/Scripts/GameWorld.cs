using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameWorld : MonoBehaviour
{
    public static GameWorld sGameWorld = GameWorld.sGameWorld;
    private GameObject player;
    private PlayerStats stats;
    public AudioSource piano;
    public GameObject[] allRooms;

    // Start is called before the first frame update
    void Awake()
    {
        allRooms[0].GetComponent<DoorSystem>().LockAll(); // Lock start room

        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();

        Debug.Assert(player != null);
        Debug.Assert(stats != null);
        Debug.Assert(allRooms != null);
    }

    // Update is called once per frame
    void Update()
    {
        int roomIndex = getPlayerCurrentRoom();
        if (roomIndex > 0)
            StartCoroutine(FadeAudioSource.StartFade(piano, 1f, 0f));
        else
            StartCoroutine(FadeAudioSource.StartFade(piano, 0.5f, .2f));
        // All rooms
        if (roomIndex > -1)
        {
            if (Input.GetKeyDown("o"))
            {
                allRooms[roomIndex].GetComponent<DoorSystem>().OpenAll();
            }
        }
        testHP();
    }

    public int getStartingHealth()
    {
        return stats.MaxHealth;
    }

    public int getPlayerCurrentRoom()
    {
        for (int i = 0; i < allRooms.Length; i++)
        {
            RoomStats stats = allRooms[i].GetComponent<RoomStats>();
            if (stats.isInRoom(player.transform.position))
                return i;
        }
        return -1;
    }

    public void testHP()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stats.incrementMaxHeath();
        }
        if (Input.GetMouseButtonDown(1))
        {
            stats.decrementMaxHeath();
        }
        if (Input.GetKeyDown("1"))
        {
            stats.gainHealth(1);
        }
        if (Input.GetKeyDown("2"))
        {
            stats.loseHealth(1);
        }

    }
}
