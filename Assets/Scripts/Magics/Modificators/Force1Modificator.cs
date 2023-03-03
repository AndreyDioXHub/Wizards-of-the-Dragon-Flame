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
        private CharacterController _character;
        [SerializeField]
        private float _speed = 10f;
        [SerializeField]
        private float _timeToDestroy = 1;

        [SerializeField]
        private float _vilosity = 0;

        [SerializeField]
        private Vector3 _resultDirection;        
        [SerializeField]
        private List<DirectionAndSpeed> _directions = new List<DirectionAndSpeed>();

        public override void Init(string key, int power, Vector3 fromHitWhereDirection)
        {
            base.Init(key, power, fromHitWhereDirection);

            _character = GetComponentInParent<CharacterController>();
            _vilosity = _speed / (_timeToDestroy * _info.power);

            for(int i=0; i< power; i++)
            {
                _directions.Add(new DirectionAndSpeed(fromHitWhereDirection, _speed, _vilosity));
            }
        }

        public override void DoSlowDown()
        {
            base.DoSlowDown();

            if (_character != null)
            {
                _resultDirection = Vector3.zero;

                /*int weight = 1;

                for(int i=0; i< _directions.Count - 1; i++)
                {
                    weight +=i;
                    _resultDirection += _directions[i].direction * _directions[i].speed; // * weight;
                }*/

                int weight = 0;
                int weightTotal = 0;

                foreach (var d in _directions)
                {
                    weight++;
                    weightTotal += weight;

                    _resultDirection += d.direction * d.speed * (d.speed / _speed);
                }

                _resultDirection = _resultDirection/(_directions.Count);

                _character.Move(_resultDirection * Time.deltaTime);

                foreach (var d in _directions)
                {
                    d.speed -= d.vilosity * Time.deltaTime;
                    d.speed = Mathf.Max(d.speed, 0);
                }
            }
        }

        public override void AddPower(int power, Vector3 fromHitWhereDirection)
        {
            base.AddPower(power, fromHitWhereDirection);
            _vilosity = _speed / (_timeToDestroy * _info.power);
            _directions.Add(new DirectionAndSpeed(fromHitWhereDirection, _speed, _vilosity));
        }

        [Serializable]
        public class DirectionAndSpeed
        {
            public Vector3 direction;
            public float speed;
            public float vilosity;
            public DirectionAndSpeed(Vector3 direction, float speed, float vilosity)
            {
                this.direction = direction;
                this.speed = speed;
                this.vilosity = vilosity;
            }
        }

    }
}
