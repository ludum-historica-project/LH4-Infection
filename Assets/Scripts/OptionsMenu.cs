﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    public GameObject container;

    public Slider sliderMasterVolume;
    public Slider sliderSoundsVolume;
    public Slider sliderMusicVolume;

    public Image resetProgressBar;

    public float resetTime = 3;

    public bool openWithESC;

    float _resetProgress;

    bool _resetting;

    bool _open = false;

    private void Start()
    {
        sliderMasterVolume.onValueChanged.AddListener(ChangeMasterVolume);
        sliderSoundsVolume.onValueChanged.AddListener(ChangeSoundsVolume);
        sliderMusicVolume.onValueChanged.AddListener(ChangeMusicVolume);
    }


    public void ChangeMasterVolume(float newVol)
    {
        Director.GetManager<SoundManager>().SetMasterVolumeScalar(newVol);
    }

    public void ChangeSoundsVolume(float newVol)
    {
        Director.GetManager<SoundManager>().SetSoundsVolumeScalar(newVol);
    }

    public void ChangeMusicVolume(float newVol)
    {
        Director.GetManager<SoundManager>().SetMusicVolumeScalar(newVol);
    }



    public void CalibrateVolumeSliders()
    {
        sliderMasterVolume.value = Director.GetManager<SoundManager>().masterVolume;
        sliderSoundsVolume.value = Director.GetManager<SoundManager>().soundsVolume;
        sliderMusicVolume.value = Director.GetManager<SoundManager>().musicVolume;
    }

    public void SetResetting(bool resetting)
    {
        _resetting = resetting;

    }

    public void Close()
    {
        container.SetActive(false);
        TimeManager.SetPause(false);
        _open = false;
    }

    public void Show()
    {
        TimeManager.SetPause(true);
        container.SetActive(true);
        _open = true;
    }

    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (openWithESC && Input.GetKeyDown(KeyCode.Escape))
        {
            if (_open)
            {
                Close();
            }
            else
            {
                Show();
            }
        }

        if (_open)
        {
            if (_resetting)
            {
                _resetProgress += Time.deltaTime;
                if (_resetProgress >= resetTime)
                {
                    //Debug.Log("Reset Progress");
                    Director.GetManager<UpgradesManager>().ResetUpgrades();
                    ReturnToMenu();
                    return;
                }
            }
            else
            {
                _resetProgress = 0;
            }
            resetProgressBar.fillAmount = _resetProgress / resetTime;
        }
    }

    private void OnDestroy()
    {
        TimeManager.SetPause(false);
    }
}
