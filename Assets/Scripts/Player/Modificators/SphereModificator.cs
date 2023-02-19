using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereModificator : MonoBehaviour
{
    public ModificatorInfo Info { get => _info; }

    [SerializeField]
    protected ModificatorInfo _info = new ModificatorInfo();
    /*protected string _key;
    [SerializeField]
    protected int _power = 1;
    [SerializeField]
    protected float _timeAction = 1;*/
    [SerializeField]
    protected float _timeActionCur;

    [SerializeField]
    protected ModificatorElementView _element;

    public virtual void Init(int power)
    {
        gameObject.name = $"m{_info.key}:{_info.power}:{_info.time}";
        _info.power = power;
    }

    public virtual int CheckCancel(string sphere, int power, out bool isCancel)
    {
        gameObject.name = $"m{_info.key}:{_info.power}:{_info.time}";
        isCancel = false;
        return 1;
    }
    public virtual void AddPower(int power)
    {
        _info.power += power;

        if (_element != null)
        {
            _element.UpdateInfo(_info.key, _info.power, 1);
        }

        gameObject.name = $"m{_info.key}:{_info.power}:{_info.time}";
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

[Serializable]
public class ModificatorInfo
{
    public string key;
    public int power;
    public float time;

}
