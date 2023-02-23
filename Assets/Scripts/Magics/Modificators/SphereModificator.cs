using com.czeeep.network.player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.czeeep.spell.modificator
{
    public class SphereModificator : MonoBehaviour
    {
        public ModificatorInfo Info { get => _info; }
        [SerializeField]
        protected PlayerInfo _playerInfo;
        [SerializeField]
        protected ModificatorInfo _info = new ModificatorInfo();
        [SerializeField]
        protected int _damage = 1;
        [SerializeField]
        protected int _maxPower = 10;
        /*protected string _key;
        [SerializeField]
        protected int _power = 1;
        [SerializeField]
        protected float _timeAction = 1;*/
        [SerializeField]
        protected float _timeActionCur;
        [SerializeField]
        protected float _filling;

        [SerializeField]
        protected ModificatorElementView _element;

        public virtual void Init(int power)
        {
            //gameObject.name = $"m{_info.key}:{_info.power}:{_info.time}";
            Named();
            _timeActionCur = 0;
            _info.power = power;
            _playerInfo = PlayerNetwork.Info;
            DoUpdatedDamage();
            DoDamage();
        }

        public virtual int CheckCancel(string sphere, int power, out bool isCancel)
        {
            //gameObject.name = $"m{_info.key}:{_info.power}:{_info.time}";

            Named();
            isCancel = false;

            //DoDamage();
            return 1;
        }

        public virtual void AddPower(int power)
        {

            _timeActionCur = 0;
            _info.power += power;

            if (_info.power > _maxPower)
            {
                _info.power = _maxPower;
            }

            UpdateInfo(1);
            Named();
            DoUpdatedDamage();
            DoDamage();
        }

        public void UpdateInfo(float fill)
        {
            if (_element != null)
            {
                _element.UpdateInfo(_info.key, _info.power, fill);
            }

        }

        public void Named()
        {
            gameObject.name = $"{"{"}\"key\": \"{_info.key}\", \"power\": {(int)_info.power}, \"direction\": {(int)_info.time}{"}"}";
        }

        // Start is called before the first frame update
        public virtual void Start()
        {
        }

        // Update is called once per frame
        public virtual void Update()
        {
            _timeActionCur += Time.deltaTime;
            _filling = 1 - _timeActionCur / _info.time;
            UpdateInfo(_filling);

            DoUpdatedDamage();

            if (_timeActionCur >= _info.time)
            {
                _timeActionCur = 0;
                _info.power --;

                if (_info.power <= 0)
                {
                    DoDamage();
                    DestroyModificator();
                }
            }
        }

        public virtual void DoUpdatedDamage()
        {

        }

        public virtual void DoDamage()
        {

        }

        public virtual void DestroyModificator()
        {
            DoDamage();
            Destroy(_element.gameObject);
            Destroy(gameObject);
        }
    }

    [Serializable]
    public class ModificatorInfo
    {
        public string key;
        public int power;
        public float time;

    }

}
