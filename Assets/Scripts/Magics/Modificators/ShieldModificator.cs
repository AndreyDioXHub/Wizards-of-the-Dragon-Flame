using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.czeeep.spell.modificator 
{
    public class ShieldModificator : SphereModificator
    {
        public override void Start()
        {
            base.Start();
        }

        public override void Init(int power)
        {
            base.Init(power);
            ModificatorView.Instance.AddNewModificator(_info.key, power, out _element);
            _element.UpdateInfo(_info.key, _info.power, 1);
        }

        public override int CheckCancel(string sphere, int power, out bool isCancel)
        {
            base.CheckCancel(sphere, power, out isCancel);

            int incomingPowerleft = 0;

            _element.UpdateInfo(_info.key, _info.power, 1);

            return incomingPowerleft;
        }

        public override void DoDamage()
        {
            base.DoDamage();
            _playerInfo.MakeShieldPointDamage(_damage * _info.power, out int left);
        }
    }
}


