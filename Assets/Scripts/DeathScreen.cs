using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeathScreen : MonoBehaviour
{
    public ScriptableEvent onPlayerHeathUpdated;

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
            Invoke("GoToUpgrades", 5);
        }
    }

    void GoToUpgrades()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UpgradesMenu");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
