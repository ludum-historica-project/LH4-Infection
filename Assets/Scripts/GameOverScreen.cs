using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverScreen : MonoBehaviour
{
    public ScriptableEvent onPlayerHeathUpdated;

    public string deathMessage;
    public string winMessage;

    public TextMeshProUGUI frontText;
    public TextMeshProUGUI backText;

    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        onPlayerHeathUpdated.OnInvoke += PlayerHealthUpdated;
    }

    void PlayerHealthUpdated(float value)
    {
        if (value <= 0)
        {
            _animator.SetTrigger("ShowScreen");
            frontText.text = backText.text = deathMessage;
            Invoke("GoToUpgrades", 5);
        }
    }

    public void Win()
    {
        Debug.Log("Yuo win");
        _animator.SetTrigger("ShowScreen");
        frontText.text = backText.text = winMessage;
        Invoke("GoToUpgrades", 5);
    }

    void GoToUpgrades()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UpgradesMenu");
    }

    // Update is called once per frame
    void OnDestroy()
    {
        onPlayerHeathUpdated.OnInvoke -= PlayerHealthUpdated;

    }
}
