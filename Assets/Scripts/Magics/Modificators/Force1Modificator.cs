using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.modificator
{
    public class Force1Modificator : SphereModificator
    {
        [SerializeField]
        private CharacterController _character;
        [SerializeField]
        private float _speed = 10f;
        [SerializeField]
        private float _timeToDestroy = 1;

        [SerializeField]
        private float _vilosity = 0;

        public override void Init(string key, int power, Vector3 fromHitWhereDirection)
        {
            base.Init(key, power, fromHitWhereDirection);

            _character = GetComponentInParent<CharacterController>();
            _vilosity = _speed / _timeToDestroy;
        }

        public override void DoSlowDown()
        {
            base.DoSlowDown();

            if (_character != null)
            {
                _character.Move((_fromHitWhereDirection) * _speed * (1f + _info.power / _info.maxPower ) * Time.deltaTime);
                _speed -= _vilosity * Time.deltaTime;
            }
        }

    }
}
