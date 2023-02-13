using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereModificator : MonoBehaviour
{
    public string Key { get => key; }

    [SerializeField]
    protected string key;
    [SerializeField]
    protected int _power = 1;

   [SerializeField]
    protected ActiveElementView _element;

    public virtual void Init(int power)
    {
        _power = power;
    }

    public virtual void CheckCancel(string sphere, out bool isCancel)
    {
        isCancel = false;
    }
    public virtual void AddPower(int power)
    {
        _power += power;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        DisableView.Instance.AddNewDisable(key, out _element);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void DestroyModificator()
    {
        Destroy(_element.gameObject);
        Destroy(this);
    }
}
