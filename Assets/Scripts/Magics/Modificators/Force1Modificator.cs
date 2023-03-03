using com.czeeep.spell.magicmodel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class Force1Modificator : SphereModificator
    {
        [SerializeField]
        private float _speed = 10f;
        [SerializeField]
        private float _timeToDestroy = 1;

        [SerializeField]
        private float _vilosity = 0;


        public override void Init(string key, int power, Vector3 fromHitWhereDirection)
        {
            base.Init(key, power, fromHitWhereDirection);

            _vilosity = _speed / (_timeToDestroy * _info.power);

            for(int i=0; i< power; i++)
            {
                _playerInfo.AddForceVector(new DirectionAndSpeed(fromHitWhereDirection, _speed, _vilosity));
            }

        }


        public override void AddPower(int power, Vector3 fromHitWhereDirection)
        {
            base.AddPower(power, fromHitWhereDirection);
            _vilosity = _speed / (_timeToDestroy * _info.power);

            _playerInfo.AddForceVector(new DirectionAndSpeed(fromHitWhereDirection, _speed, _vilosity));
        }
    }
}
