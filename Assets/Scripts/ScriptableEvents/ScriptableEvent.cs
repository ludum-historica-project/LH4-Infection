using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newScriptableEvent", menuName = "Scriptables/Event")]
public class ScriptableEvent : ScriptableObject
{
    public System.Action<float> OnInvoke = (n) => { };

    public void Invoke(float n)
    {
        OnInvoke(n);
    }
}
