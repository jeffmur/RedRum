using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
    }
}
