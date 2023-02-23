using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.czeeep.spell.modificator 
{
    public class FrezeModificator : SphereModificator
    {
        public override void Start()
        {
            base.Start();
            //key = "fire";
        }
        public override void Init(int power)
        {
            base.Init(power);
            MagicModel.Instance.ReturnAllSphereToInventory("fire");
            MagicModel.Instance.ReturnAllSphereToInventory("steam");
            MagicModel.Instance.ReturnAllSphereToInventory("razor");
            ModificatorView.Instance.AddNewModificator(_info.key, power, out _element);
            _element.UpdateInfo(_info.key, _info.power, 1);
            DoDamage();

        }
        public override void AddPower(int power)
        {
            base.AddPower(power);

            if (_info.power > _maxPower)
            {
                _info.power = _maxPower;
            }

            UpdateInfo(1);
            Named();

            DoDamage();
        }
        /*public override void AddPower(int power)
        {
            _power += power;

            if (_element != null)
            {
                _element.UpdateInfo(_key, power);
            }
        }*/

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
                    //DoDamage();
                    DestroyModificator();
                }
            }/*
            else if (sphere == SpheresElements.razor.ToString())
            {
                incomingPowerleft = 0;
                isCancel = true;
                //do razor damage
                Debug.Log($"do razor damage {power}");
                //DestroyModificator();
            }*/

            _element.UpdateInfo(_info.key, _info.power, 1);
            DoDamage();
            return incomingPowerleft;
        }
        public override void DoDamage()
        {
            base.DoDamage();

            float slowdown = 1 - (float)((_info.power) * (_info.power)) / (float)(_maxPower * _maxPower);

            slowdown = slowdown < 0.1f ? 0.1f : slowdown;

            _playerInfo.SpeedFraud(_info.key, slowdown);

            if (_info.power <= 0)
            {
                _playerInfo.SpeedFraud(_info.key, 1);
            }

            _playerInfo.MakeDamage(_damage * (int)_info.power / 2);
        }
    }
}

