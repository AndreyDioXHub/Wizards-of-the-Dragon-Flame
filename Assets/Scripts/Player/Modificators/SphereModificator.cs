using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereModificator : MonoBehaviour
{
    public string Key { get => _key; }
    public int Power { get => _power; }

    [SerializeField]
    protected string _key;
    [SerializeField]
    protected int _power = 1;
    [SerializeField]
    protected float _timeAction = 1;
    [SerializeField]
    protected float _timeActionCur;

    [SerializeField]
    protected ModificatorElementView _element;

    public virtual void Init(int power)
    {
        gameObject.name = $"m:{_key}:{power}";
        _power = power;
    }

    public virtual int CheckCancel(string sphere, int power, out bool isCancel)
    {
        isCancel = false;
        return 1;
    }
    public virtual void AddPower(int power)
    {
        _power += power;

        if (_element != null)
        {
            _element.UpdateInfo(_key, _power, 1);
        }

        gameObject.name = $"m:{_key}:{_power}";
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
    public virtual void DoDamage()
    {

    }

    public virtual void DestroyModificator()
    {
        Destroy(_element.gameObject);
        Destroy(gameObject);
    }
}
