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
            _info.key = "fire";
        }
        public override void Init(int power)
        {
            base.Init(power);
            MagicModel.Instance.ReturnAllSphereToInventory("water");
            MagicModel.Instance.ReturnAllSphereToInventory("steam");
            ModificatorView.Instance.AddNewModificator(_info.key, power, out _element);
            _element.UpdateInfo(_info.key, _info.power, 1);

            //DoDamage();

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

            //DoDamage();
            return incomingPowerleft;
        }

        public override void DoDamage()
        {
            base.DoDamage();
            _playerInfo.MakeDamage(_damage*_info.power);
            //_playerInfo.SetStun(_info.key, true);
        }
        public override void DestroyModificator()
        {
            base.DestroyModificator();
            //_playerInfo.SetStun(_info.key, false);
        }
    }

}
