using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class UnArmorModificator : SphereModificator
    {

        public override void DoDamage()
        {
            //base.DoDamage();
            _playerInfo.UnArmor();
        }
    }
}
