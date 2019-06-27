using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public OptionsMenu optionsMenu;

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UpgradesMenu");
    }

    public void ShowOptions()
    {
        optionsMenu.Show();
    }

    public void Quit()
    {
        Application.Quit();
    }
}

