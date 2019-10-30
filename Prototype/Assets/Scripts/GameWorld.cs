using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.Audio;
=======
>>>>>>> andy

public class GameWorld : MonoBehaviour
{
    private GameObject player;
    private PlayerStats stats;
<<<<<<< HEAD
    public AudioSource piano;
    public GameObject[] allRooms;
=======
>>>>>>> andy

    // Start is called before the first frame update
    void Awake()
    {
<<<<<<< HEAD
        allRooms[0].GetComponent<DoorSystem>().LockAll();
=======
>>>>>>> andy
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
        Debug.Assert(player != null);
        Debug.Assert(stats != null);
<<<<<<< HEAD
        Debug.Assert(allRooms != null);
=======
>>>>>>> andy
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        int roomIndex = getPlayerCurrentRoom();
        if (roomIndex > 0)
            StartCoroutine(FadeAudioSource.StartFade(piano, 1f, 0f));
        else
            StartCoroutine(FadeAudioSource.StartFade(piano, 0.5f, .2f));
        if (roomIndex > -1)
        {
            if (Input.GetKeyDown("o"))
            {
                allRooms[roomIndex].GetComponent<DoorSystem>().OpenAll();
            }
        }
=======
        testHP();
>>>>>>> andy
    }

    public int getStartingHealth()
    {
        return stats.MaxHealth;
    }

<<<<<<< HEAD
    public int getPlayerCurrentRoom()
    {
        for(int i = 0; i < allRooms.Length; i++)
        {
            RoomStats stats = allRooms[i].GetComponent<RoomStats>();
            if (stats.isInRoom(player.transform.position))
                return i;
        }
        return -1;
=======
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
>>>>>>> andy
    }
}
