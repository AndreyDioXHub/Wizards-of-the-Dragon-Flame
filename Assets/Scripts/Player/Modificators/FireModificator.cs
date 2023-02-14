using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModificator : SphereModificator
{
    public override void Start()
    {
        base.Start();
        //key = "fire";
    }
    public override void Init(int power)
    {
        base.Init(power);
        ModificatorView.Instance.AddNewModificator(_key, power, out _element);
        _element.UpdateInfo(_key, _power);
    }

    public override int CheckCancel(string sphere, int power, out bool isCancel)
    {
        base.CheckCancel(sphere, power, out isCancel);

        int incomingPowerleft = 0;

        if (sphere == SpheresElements.water.ToString())
        {
            incomingPowerleft = (power - _power) <= 0 ? 0 : power - _power;
            
            _power -= power;
            isCancel = true; 

            if (_power <= 0)
            {
                DestroyModificator();
            }
        }

        _element.UpdateInfo(_key, _power);
        return incomingPowerleft;
    }
}
