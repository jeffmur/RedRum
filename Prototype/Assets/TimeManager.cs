using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float slowdownFactor = 0.05f;
    private float slowdownLength = 2f;
    bool alwaysSlow = false;

    private void Update()
    {
        //swap this to negative to freeze time
        if (alwaysSlow == false)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            if (Time.timeScale == 1f)
                ResetTime();
        }

    }

    public void PermSlow(float slowFactor)
    {
        alwaysSlow = true;
        slowdownFactor = slowFactor;
        DoSlowMotion(100);
    }

    public void DoSlowMotion(int length)
    {
        slowdownLength = length;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.unscaledDeltaTime;
    }

    public void ResetTime()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.unscaledDeltaTime;
    }
}
