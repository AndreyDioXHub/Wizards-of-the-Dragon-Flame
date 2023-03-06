using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class CallOnEnable : MonoBehaviour {
    public UnityEvent EnableEvent;
    public UnityEvent DisableEvent;
    public UnityEvent StartEvent;

    public UnityEvent OnDelayEnable;
    public UnityEvent OnDelayStart;
    [Tooltip("Seconds to delay event after start")]
    public float DelaySeconds = 1.0f;

    bool inited = false;
    
    public void OnEnable() {
        EnableEvent.Invoke();
        StartCoroutine(OnEnableDelay());
    }
    private IEnumerator OnEnableDelay()
    {
        yield return new WaitForSeconds(DelaySeconds);
        OnDelayEnable.Invoke();
    }

    public void OnDisable() {
        
        DisableEvent.Invoke(); 
    }

    public void Start() {
        inited = true;
        StartEvent.Invoke();
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay() {
        yield return new WaitForSeconds(DelaySeconds);
        OnDelayStart.Invoke();
    }
}
