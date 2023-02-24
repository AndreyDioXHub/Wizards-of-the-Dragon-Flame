using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class WaterModificator : SphereModificator
    {

        public override void Start()
        {
            base.Start();
        }

        public override void Init(int power)
        {
            base.Init(power);
            MagicModel.Instance.ReturnAllSphereToInventory(MagicConst.FIRE);
            MagicModel.Instance.ReturnAllSphereToInventory(MagicConst.STEAM);
            MagicModel.Instance.ReturnAllSphereToInventory(MagicConst.RAZOR);
            ModificatorView.Instance.AddNewModificator(_info.key, power, out _element);
            _element.UpdateInfo(_info.key, _info.power, 1);
        }

        public override int CheckCancel(string sphere, int power, out bool isCancel)
        {
            base.CheckCancel(sphere, power, out isCancel);

            int incomingPowerleft = 0;

            if (sphere == SpheresElements.fire.ToString())
            {
                incomingPowerleft = (power - _info.power) <= 0 ? 0 : power - _info.power;

                _info.power -= power;
                isCancel = true;

                if (_info.power <= 0)
                {
                    DestroyModificator();
                }
            }
            else if (sphere == SpheresElements.razor.ToString())
            {
                incomingPowerleft = 0;
                isCancel = true;
                //do razor damage
                Debug.Log($"do razor damage {power}");
                _playerInfo.UnArmor();
                _playerInfo.MakeHitPointDamage(power);
            }

            _element.UpdateInfo(_info.key, _info.power, 1);

            return incomingPowerleft;
        }


        public override void DoUpdatedDamage()
        {
            base.DoUpdatedDamage();

            float slowdown = 1 - (float)((_info.power) * (_info.power)) / (float)(_maxPower * _maxPower);

            slowdown = slowdown < 0.1f ? 0.1f : slowdown;

            _playerInfo.SpeedFraud(_info.key, slowdown);

            if (_info.power <= 0)
            {
                _playerInfo.SpeedFraud(_info.key, 1);
            }
        }


        public override void DoDamage()
        {
            base.DoDamage();
        }
    }


}