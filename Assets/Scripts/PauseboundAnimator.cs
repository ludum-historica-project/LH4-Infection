using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PauseboundAnimator : MonoBehaviour
{
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = !TimeManager.paused;
        TimeManager.OnChangePauseState += SetAnimatorEnabled;
    }

    void SetAnimatorEnabled(bool paused)
    {
        Debug.Log("Disabling animator: " + gameObject.name);
        _animator.enabled = !paused;
    }

    void OnDestroy()
    {
        TimeManager.OnChangePauseState -= SetAnimatorEnabled;
    }
}
