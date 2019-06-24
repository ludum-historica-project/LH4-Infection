using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newScriptableEvent", menuName = "Scriptables/Event")]
public class ScriptableEvent : ScriptableObject
{
    public System.Action<object> OnInvoke = (o) => { };

    public void Invoke(object o)
    {
        OnInvoke(o);
    }
}
