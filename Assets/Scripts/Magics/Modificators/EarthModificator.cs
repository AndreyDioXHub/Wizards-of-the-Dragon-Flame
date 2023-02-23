using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class EarthModificator : SphereModificator
    {

        public override void Init(int power)
        {
            base.Init(power);
            MagicModel.Instance.ReturnAllSphereToInventory();
            ModificatorView.Instance.AddNewModificator(_info.key, power, out _element);
            _element.UpdateInfo(_info.key, _info.power, 1);
            //DoDamage();
            //MagicModel.Instance.AddModificator("fire", 1);
            DestroyModificator();
            //DoUpdatedDamage();
            //DoDamage();

        }
        public override void DoDamage()
        {
            base.DoDamage();
            _playerInfo.MakeDamage(_damage * _info.power);
        }
    }

}
