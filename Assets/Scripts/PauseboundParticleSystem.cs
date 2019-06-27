using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class PauseboundParticleSystem : MonoBehaviour
{
    ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        if (TimeManager.paused)
        {
            _particleSystem.Pause();
        }
        else
        {
            _particleSystem.Play();
        }
        TimeManager.OnChangePauseState += SetParticlesEnabled;
    }

    void SetParticlesEnabled(bool paused)
    {
        if (paused)
        {
            _particleSystem.Pause();
        }
        else
        {
            _particleSystem.Play();
        }
    }

    void OnDestroy()
    {
        TimeManager.OnChangePauseState -= SetParticlesEnabled;
    }
}
