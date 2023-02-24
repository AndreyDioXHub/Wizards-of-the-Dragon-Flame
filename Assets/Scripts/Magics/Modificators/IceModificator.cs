using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class IceModificator : SphereModificator
    {


        public override void Start()
        {
            base.Start();
            _info.key = MagicConst.ICE;
        }

        public override void Init(int power)
        {
            base.Init(power);
            MagicModel.Instance.ReturnAllSphereToInventory();
            ModificatorView.Instance.AddNewModificator(_info.key, power, out _element);
            _element.UpdateInfo(_info.key, _info.power, 1);
            MagicModel.Instance.AddModificator(MagicConst.STUN, _info.power);
            MagicModel.Instance.AddModificator(MagicConst.FREZE, _info.power);

        }
        public override int CheckCancel(string sphere, int power, out bool isCancel)
        {
            base.CheckCancel(sphere, power, out isCancel);

            int incomingPowerleft = power;

            if (sphere == SpheresElements.none.ToString())
            {
                incomingPowerleft = (power - _info.power) <= 0 ? 0 : power - _info.power;

                _info.power -= power;
                isCancel = true;

                if (_info.power <= 0)
                {
                    DestroyModificator();
                }
            }

            _element.UpdateInfo(_info.key, _info.power, 1);
            return incomingPowerleft;
        }

        public override void DoDamage()
        {
            //base.DoDamage();
            _playerInfo.MakeDamage(_damage * _info.power);
        }
    }
}

