using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterModificator : SphereModificator
{
    public override void Start()
    {
        base.Start();
        //key = "fire";
    }
    public override void Init(int power)
    {
        base.Init(power);
        DisableView.Instance.AddNewDisable(_key, out _element);
        _element.Init(_key);
    }

    public override int CheckCancel(string sphere, int power, out bool isCancel)
    {
        base.CheckCancel(sphere, power, out isCancel);

        int incomingPowerleft = 0;

        if (sphere == SpheresElements.fire.ToString())
        {
            incomingPowerleft = (power - _power) <= 0 ? 0 : power - _power;

            _power -= power;
            isCancel = true;

            if (_power <= 0)
            {
                DestroyModificator();
            }
        }

        return incomingPowerleft;
    }
}
