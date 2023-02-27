using com.czeeep.network.player;
using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class StunModificator : SphereModificator
    {
        public override void Init(string key, int power)
        {
            base.Init(key, power);
            MagicModel.Instance.ReturnAllSphereToInventory(); 
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

