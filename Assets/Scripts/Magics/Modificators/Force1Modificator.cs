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
        private LayerMask _env;

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


                /*
                if (_characterVilosity > _thresholdTurn && _swaped)
                {
                    Debug.Log("unswap");
                    _swaped = false;
                }*/

                _resultDirection = Vector3.zero;

                float weight = 0;
                float weightTotal = 0;

                foreach (var d in _directions)
                {
                    weight ++;
                    weightTotal += weight;

                    _resultDirection += d.direction * d.speed * weight;
                }

                _resultDirection = _resultDirection / (weightTotal);

                _character.Move(_resultDirection * Time.deltaTime);

                foreach (var d in _directions)
                {
                    d.speed -= d.vilosity * Time.deltaTime;
                    d.speed = Mathf.Max(d.speed, 0);
                }

                Debug.DrawLine(transform.position, transform.position + _resultDirection.normalized, Color.blue);

                if (Physics.Raycast(transform.position, _resultDirection.normalized, out RaycastHit hit, 1, _env))
                {
                    Debug.Log($"swap {_directions.Count} ");

                    foreach (var d in _directions)
                    {
                        Debug.Log($"swap {d.direction} ");
                        d.direction = -1f * d.direction / 2f;
                        Debug.Log($"swap {d.direction} ");
                    }
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
