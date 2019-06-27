using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager
{
    public static bool paused { get; private set; } = false;



    public static System.Action<bool> OnChangePauseState = (p) => { };

    public static void TogglePause()
    {
        paused = !paused;
        Physics.autoSimulation = Physics2D.autoSimulation = !paused;
        OnChangePauseState(paused);
    }

    public static void SetPause(bool paused)
    {
        if (TimeManager.paused != paused)
        {
            TimeManager.paused = paused;
            OnChangePauseState(paused);
        }
    }

    public static float deltaTime
    {
        get
        {
            if (paused) return 0;
            return Time.deltaTime;
        }
    }
    public static float fixedDeltaTime
    {
        get
        {
            if (paused) return 0;
            return Time.fixedDeltaTime;
        }
    }

}
