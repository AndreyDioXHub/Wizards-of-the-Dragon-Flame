using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class RazorModificator : SphereModificator
    {

        public override void Update()
        {
            base.Update();

            _timeActionCur += Time.deltaTime;

            if (_element != null)
            {
                _element.UpdateInfo(_info.key, _info.power, 1 - _timeActionCur / _info.time);
            }

            if (_timeActionCur >= _info.time)
            {
                DestroyModificator();
            }
        }

        public override void DoDamage()
        {
            base.DoDamage();
            _playerInfo.MakeDamage(_damage * _info.power);
            //Debug.Log($"do razor damage {_info.power}");
        }

        public override void Init(int power)
        {
            base.Init(power);
            MagicModel.Instance.ReturnAllSphereToInventory("water");
            ModificatorView.Instance.AddNewModificator(_info.key, power, out _element);
            _element.UpdateInfo(_info.key, _info.power, 1);
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


}