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

        }

        public override void DoDamage()
        {
            base.DoDamage();
            _playerInfo.UnArmor();
           // _playerInfo.MakeShieldPointDamage(100, out int left);
            _playerInfo.MakeHitPointDamage(_damage * _info.power);
            //Debug.Log($"do razor damage {_info.power}");
        }

        public override void Init(int power)
        {
            base.Init(power);
            MagicModel.Instance.ReturnAllSphereToInventory("water");
            ModificatorView.Instance.AddNewModificator(_info.key, power, out _element);
            UpdateInfo(1);
            DoDamage();
            //_element.UpdateInfo(_info.key, _info.power, 1);
        }

        public override int CheckCancel(string sphere, int power, out bool isCancel)
        {
            base.CheckCancel(sphere, power, out isCancel);

            int incomingPowerleft = 0;

            if (sphere == SpheresElements.water.ToString())
            {
                incomingPowerleft = 0;
                isCancel = true;
                //do razor damage
                DoDamage();
                AddPower(1);
                //Debug.Log($"do razor damage {power}");
            }

            return incomingPowerleft;
        }
    }


}