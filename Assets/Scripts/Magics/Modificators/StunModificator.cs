using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class StunModificator : SphereModificator
    {
        public override void Start()
        {
            base.Start();
            _info.key = MagicConst.STUN;
        }

        public override void Init(int power)
        {
            base.Init(power);
            MagicModel.Instance.ReturnAllSphereToInventory();
            ModificatorView.Instance.AddNewModificator(_info.key, power, out _element);
            _element.UpdateInfo(_info.key, _info.power, 1);

        }

        public override void DoDamage()
        {
            //base.DoDamage();
            _playerInfo.SetStun(_info.key, true);
        }

        public override void DestroyModificator()
        {
            base.DestroyModificator();
            _playerInfo.SetStun(_info.key, false);
        }
    }
}

