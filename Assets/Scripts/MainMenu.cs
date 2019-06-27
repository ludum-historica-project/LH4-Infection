using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UpgradesMenu");
    }

    public void ShowOptions()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}

