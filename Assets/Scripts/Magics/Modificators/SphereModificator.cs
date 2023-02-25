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
            Named();
            _timeActionCur = 0;
            _info.power = power;
            _playerInfo = PlayerNetwork.Info;
            DoDamage();
        }

        public virtual int CheckCancel(string sphere, int power, out bool isCancel)
        {
            Named();
            isCancel = false;

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
            /*DoUpdatedDamage();
            DoDamage();*/
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
                    DestroyModificator();
                }
            }
        }

        public virtual void DoUpdatedDamage()
        {

        }

        public virtual void DoDamage()
        {
            Debug.Log("DoDamage");
        }

        public virtual void DestroyModificator()
        {
            Destroy(_element.gameObject);
            Destroy(gameObject);
        }
    }

    [Serializable]
    public class ModificatorInfo
    {
        public int tir;
        public string type;
        public string key;
        public float time;
        public int power;
        public int maxPower;
        public float damageFull;
        public float damage;
        public float multiplierHitPoint;
        public float multiplierShieldPoint;
        public float slowdown;
        public bool unArmor;
        public List<string> additionalEffects = new List<string>();
        public List<string> meltCastSequences = new List<string>();

        public ModificatorInfo(int tir, string type, string key, float time, int maxPower,
            float damageFull, float multiplierHitPoint, float multiplierShieldPoint, float slowdown,
            bool unArmor, List<string> additionalEffects, List<string> meltCastSequences)
        {
            this.tir = tir;
            this.type = type;
            this.key = key;
            this.time = time;
            this.power = 1;
            this.maxPower = maxPower;
            this.damageFull = damageFull;
            this.damage = (damageFull/ maxPower)/ time;
            this.multiplierHitPoint = multiplierHitPoint;
            this.multiplierShieldPoint = multiplierShieldPoint;
            this.slowdown = slowdown;
            this.unArmor = unArmor;
            this.additionalEffects = additionalEffects;
            this.meltCastSequences = meltCastSequences;
        }
    }
    //tir, key, time, power, maxPower, damageFull, multiplierHitPoint, multiplierShieldPoint, slowdown, unArmor,
    //List<string> additionalEffects, type, List<string> meltCastSequences
}
