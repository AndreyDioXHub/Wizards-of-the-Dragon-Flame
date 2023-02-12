using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField]
    protected Tick _tick;
    [SerializeField]
    protected CastDirection _direction;
    [SerializeField]
    protected int _power;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int power, CastDirection direction, Tick tick)
    {
        _power = power;
        _direction = direction;
        _tick = tick;
    }

    public void DestroyMagic()
    {

    }
}
