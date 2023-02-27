using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class WaterModificator : SphereModificator
    {
        public override int CheckCancel(string sphere, int power, out bool isCancel)
        {
            base.CheckCancel(sphere, power, out isCancel);

            int incomingPowerleft = 0;

            if (sphere == SpheresElements.fire.ToString())
            {
                incomingPowerleft = (power - _info.power) <= 0 ? 0 : power - _info.power;

                _info.power -= power;
                isCancel = true;

                if (_info.power <= 0)
                {
                    DestroyModificator();
                }
            }
            else if (sphere == SpheresElements.razor.ToString())
            {
                incomingPowerleft = 0;
                isCancel = true;
                //do razor damage
                Debug.Log($"do razor damage {power}");
                //_playerInfo.UnArmor();
                //_playerInfo.MakeHitPointDamage(power);
            }

            _element.UpdateInfo(_info.key, _info.power, 1);

            return incomingPowerleft;
        }
    }


}