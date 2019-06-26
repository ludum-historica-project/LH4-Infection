using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIFillBar : MonoBehaviour
{
    public ScriptableEvent OnValueChangeEvent;
    public Image bar;
    public FloatReference maxValue;
    public float min = 0;
    public float max = 1;
    // Start is called before the first frame update
    void Start()
    {
        OnValueChangeEvent.OnInvoke += ValueChanged;
    }

    private void OnDestroy()
    {
        OnValueChangeEvent.OnInvoke -= ValueChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ValueChanged(float number)
    {
        bar.fillAmount = Mathf.Lerp(min, max, (number) / maxValue.Value);
    }
}
