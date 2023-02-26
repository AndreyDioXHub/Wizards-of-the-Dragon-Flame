using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class RazorModificator : SphereModificator
    {

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();

        }

        public override void DoDamage()
        {
            //base.DoDamage();
            _playerInfo.UnArmor();
            _playerInfo.MakeHitPointDamage(_info.damage * _info.power);
        }

        public override void Init(string key, int power)
        {
            base.Init(key, power);
            MagicModel.Instance.ReturnAllSphereToInventory(MagicConst.WATER);
            ModificatorView.Instance.AddNewModificator(_info.key, power, out _element);
            UpdateInfo(1);
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
                _timeActionCur = 0;
                //AddPower(1);
                //Debug.Log($"do razor damage {power}");
            }

            return incomingPowerleft;
        }
    }


}