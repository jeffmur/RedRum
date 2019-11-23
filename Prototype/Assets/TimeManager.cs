using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float slowdownFactor = 0.05f;
    private float slowdownLength = 2f;
    private bool alwaysSlow = false;

    private void Update()
    {
        //swap this to negative to freeze time
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        Debug.Log(Time.timeScale);
        Debug.Log(slowdownLength);
        if (Time.timeScale == 1f)
            ResetTime();
        }

    public void DoSlowMotion(int length)
    {
        slowdownLength = length;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.unscaledDeltaTime;
    }
    public void ResetTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale;
    }
}
