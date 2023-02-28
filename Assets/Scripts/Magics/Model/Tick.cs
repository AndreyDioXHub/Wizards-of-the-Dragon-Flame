using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tick : MonoBehaviour
{
    public UnityEvent OnTickPassed = new UnityEvent();

    public float TickFill { get => (1 - _tickTimeCur/ _tickTime); }

    [SerializeField]
    private float _tickTime = 1;
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

    public void UpdateInfo(float tickTime = 1, float tickTimeCur = 0, bool mayUpdateTick = true, bool passTick = false)
    {
        _tickTime = tickTime;
        _tickTimeCur = tickTimeCur;
        _mayUpdateTick = mayUpdateTick;
        _passTick = passTick;
    }

    // Update is called once per frame
    private void Update()
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
