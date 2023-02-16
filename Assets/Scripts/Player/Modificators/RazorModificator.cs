using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorModificator : SphereModificator
{

    public override void Update()
    {
        base.Update();

        _timeActionCur += Time.deltaTime;

        if(_element != null)
        {
            _element.UpdateInfo(_key, _power, 1 - _timeActionCur/ _timeAction);
        }

        if(_timeActionCur >= _timeAction)
        {
            DestroyModificator();
        }
    }

    public override void DoDamage()
    {
        base.DoDamage();
        Debug.Log($"do razor damage {_power}");
    }

    public override void Init(int power)
    {
        base.Init(power);
        MagicModel.Instance.ReturnAllSphereToInventory("water");
        ModificatorView.Instance.AddNewModificator(_key, power, out _element);
        _element.UpdateInfo(_key, _power, 1);
        _timeActionCur = 0;
    }

    public override int CheckCancel(string sphere, int power, out bool isCancel)
    {
        base.CheckCancel(sphere, power, out isCancel);

        int incomingPowerleft = 0;

        if (sphere == SpheresElements.water.ToString())
        {
            incomingPowerleft = 0;
            isCancel = true;
            _timeActionCur = 0;
            //do razor damage
            Debug.Log($"do razor damage {power}");
        }
        
        return incomingPowerleft;
    }
}