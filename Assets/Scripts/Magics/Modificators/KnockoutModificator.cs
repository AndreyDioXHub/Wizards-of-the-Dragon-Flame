using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class KnockoutModificator : SphereModificator
    {
        public override void Init(string key, int power, Vector3 fromHitWhereDirection)
        {
            base.Init(key, power, fromHitWhereDirection);
            MagicModel.Instance.ReturnAllSphereToInventory();

        }

        public override int CheckCancel(string sphere, int power, out bool isCancel)
        {
            base.CheckCancel(sphere, power, out isCancel);

            int incomingPowerleft = 0;

            if (sphere.Equals(SpheresElements.unmodificator.ToString()))
            {
                incomingPowerleft = (power - _info.power) <= 0 ? 0 : power - _info.power;

                _info.power -= power;
                isCancel = true;

                if (_info.power <= 0)
                {
                    DestroyModificator();
                }
            }
            /*else 
            {
                incomingPowerleft = 0;
                isCancel = true;
            }*/

            //_element.UpdateInfo(_info.key, _info.power, 1);
            return incomingPowerleft;
        }

        public override void InfoPowerLessZero()
        {
            if (_info.power <= 0)
            {
                _playerInfo.SetDeadRevival(true);
                DestroyModificator();
            }
        }

        public override void DoSlowDown()
        {
            if (_info.slowdown != 0)
            {
                float slowdown = _info.slowdown;

                _playerInfo.SpeedFraud(_info.key, slowdown);

                if (_info.power <= 0)
                {
                    _playerInfo.SpeedFraud(_info.key, 1);
                }
            }

        }

        public override void DestroyModificator()
        {
            base.DestroyModificator();
            _playerInfo.SetKnockout(false);
        }
    }

}
