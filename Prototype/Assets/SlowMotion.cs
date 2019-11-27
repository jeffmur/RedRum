using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SlowMotion
{
    public static IEnumerator DoSlowMotion(float length, float slowFactor)
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = Time.unscaledDeltaTime;
        while (Time.timeScale < 1f)
        {
            Debug.Log(Time.timeScale);
            // linearly increase the timescale
            Time.timeScale += (1f / length) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            yield return null;
        }
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.unscaledDeltaTime;
    }
}
