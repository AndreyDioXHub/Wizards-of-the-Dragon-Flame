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
    private bool _mayUpdateTick;

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
        if (_mayUpdateTick)
        {
            _mayUpdateTick = false;
            _tickTimeCur = 0;
        }
    }
}
