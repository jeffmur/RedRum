using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRegister : MonoBehaviour
{
    public int i; // room index
    public Vector3 s; // scale
    public int RoomIndex { get => i; set => i = value; }

    public Vector3 OriginalScale { get => s; set => s = value; }
}
