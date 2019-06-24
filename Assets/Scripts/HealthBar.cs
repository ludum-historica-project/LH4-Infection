using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public ScriptableEvent OnPlayerHealthChangeEvent;
    public Image bar;
    public FloatReference playerMaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        OnPlayerHealthChangeEvent.OnInvoke += PlayerHealthChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayerHealthChanged(object health)
    {
        bar.fillAmount = ((float)health) / playerMaxHealth.Value;
    }
}
