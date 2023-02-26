using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.czeeep.spell.modificator
{
    public class FireModificator : SphereModificator
    {
        public override void Start()
        {
            base.Start();
        }

        public override void Init(string key, int power)
        {
            base.Init(key, power);
            MagicModel.Instance.ReturnAllSphereToInventory(MagicConst.WATER);
            MagicModel.Instance.ReturnAllSphereToInventory(MagicConst.STEAM);
            ModificatorView.Instance.AddNewModificator(_info.key, power, out _element);
            _element.UpdateInfo(_info.key, _info.power, 1);

        }

        public override int CheckCancel(string sphere, int power, out bool isCancel)
        {
            base.CheckCancel(sphere, power, out isCancel);

            int incomingPowerleft = 0;

            if (sphere == SpheresElements.water.ToString())
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
            _playerInfo.MakeDamage(_info.damage *_info.power);
        }
        public override void DestroyModificator()
        {
            base.DestroyModificator();
        }
    }

}
