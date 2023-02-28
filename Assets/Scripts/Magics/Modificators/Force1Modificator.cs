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
        private float _speed = 1000f;


        public override void Init(string key, int power, Vector3 fromHitWhereDirection)
        {
            base.Init(key, power, fromHitWhereDirection);
            _character = GetComponentInParent<CharacterController>();   
        }

        public override void DoSlowDown()
        {
            base.DoSlowDown();

            if (_character != null)
            {
                _character.Move((-_fromHitWhereDirection) * _speed * (1f + 1f/_info.power) * Time.deltaTime);
            }
        }

    }
}
