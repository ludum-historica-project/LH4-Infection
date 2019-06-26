using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public ScriptableEvent onPlayerHealthChange;
    public FloatReference maxHealth;
    public TMPro.TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = maxHealth.Value + " / " + maxHealth.Value;
        onPlayerHealthChange.OnInvoke += UpdateHealth;
    }

    private void OnDestroy()
    {
        onPlayerHealthChange.OnInvoke -= UpdateHealth;
    }

    void UpdateHealth(float newHealth)
    {
        text.text = (int)newHealth + " / " + maxHealth.Value;

    }
}
