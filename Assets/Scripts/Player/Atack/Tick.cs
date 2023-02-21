using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tick : MonoBehaviour
{
    public UnityEvent OnTickPassed;

    [SerializeField]
    private float _tickTime = 2;
    [SerializeField]
    private float _tickTimeCur;

    [SerializeField]
    private bool _mayUpdateTick = true;
    [SerializeField]
    private bool _passTick = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _tickTimeCur += Time.deltaTime;

        if(_tickTimeCur > _tickTime)
        {
            _tickTimeCur = 0;
            _mayUpdateTick = true;
            OnTickPassed?.Invoke();
        }
    }

    [ContextMenu("Update Tick")]
    public void UpdateTick()
    {
        //Debug.Log("UpdateTick");
        if (_mayUpdateTick)
        {
            if (_passTick)
            {
                OnTickPassed?.Invoke();
            }

            _mayUpdateTick = false;
            _tickTimeCur = 0;
        }
    }
}
