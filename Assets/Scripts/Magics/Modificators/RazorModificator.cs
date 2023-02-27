using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class RazorModificator : SphereModificator
    {
        public override int CheckCancel(string sphere, int power, out bool isCancel)
        {
            base.CheckCancel(sphere, power, out isCancel);

            int incomingPowerleft = 0;

            if (sphere == SpheresElements.water.ToString())
            {
                incomingPowerleft = 0;
                isCancel = true;
                //do razor damage
                DoDamage();
                _timeActionCur = 0;
                //AddPower(1);
                //Debug.Log($"do razor damage {power}");
            }

            return incomingPowerleft;
        }
    }


}