using com.czeeep.network.player;
using com.czeeep.spell.magicmodel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.czeeep.spell.modificator
{
    //[RequireComponent(typeof(Tick))]
    public class SphereModificator : MonoBehaviour
    {

        public ModificatorInfo Info { get => _info; }
        [SerializeField]
        protected PlayerInfo _playerInfo;
        [SerializeField]
        protected Tick _tick;
        [SerializeField]
        protected ModificatorInfo _info = new ModificatorInfo();
        [SerializeField]
        protected float _timeActionCur;
        [SerializeField]
        protected float _filling;
        [SerializeField]
        protected Vector3 _fromHitWhereDirection;

        [SerializeField]
        protected ModificatorElementView _element;

        private void Awake()
        {
            
        }

        public virtual void Init(string key, int power, Vector3 fromHitWhereDirection)
        {
            Named();

            _timeActionCur = 0;
            _info = new ModificatorInfo(key, power);
            _playerInfo = PlayerNetwork.Info;
            _fromHitWhereDirection = fromHitWhereDirection;
            Debug.Log($"fromHitWhereDirection {fromHitWhereDirection}" );
            if (_info.power > _info.maxPower)
            {
                _info.power = _info.maxPower;
            }

            foreach (var hiding in _info.hidingSpheres)
            {
                MagicModel.Instance.ReturnAllSphereToInventory(hiding);
            }

            ModificatorView.Instance.AddNewModificator(_info.key, _info.power, out _element);
            _element.UpdateInfo(_info.key, _info.power, 1);

            foreach(var addMod in _info.additionalEffects)
            {
                MagicModel.Instance.AddModificator(addMod, _info.power, _fromHitWhereDirection);
            }

            DoDamage();

            _tick = gameObject.AddComponent<Tick>();
            _tick.OnTickPassed.AddListener(DoDamage);
        }

        public virtual int CheckCancel(string sphere, int power, out bool isCancel)
        {
            isCancel = false;

            int incomingPowerleft = power;

            if (sphere == SpheresElements.none.ToString())
            {
                incomingPowerleft = (power - _info.power) <= 0 ? 0 : power - _info.power;

                _info.power -= power;
                isCancel = true;

                if (_info.power <= 0)
                {
                    DestroyModificator();
                }
            }

            _element.UpdateInfo(_info.key, _info.power, 1);
            return incomingPowerleft;
        }

        public virtual void AddPower(int power, Vector3 fromHitWhereDirection)
        {

            _timeActionCur = 0;
            _info.power += power;

            if (_info.power > _info.maxPower)
            {
                _info.power = _info.maxPower;
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

            DoSlowDown();

            if (_timeActionCur >= _info.time)
            {
                _timeActionCur = 0;
                _info.power --;

                InfoPowerLessZero();
            }
        }

        public virtual void InfoPowerLessZero()
        {
            if (_info.power <= 0)
            {
                DestroyModificator();
            }
        }

        public virtual void DoSlowDown()
        {
            if(_info.slowdown != 0)
            {

                float powerSquar = (float)(_info.power - 1 + _filling) * (_info.power - 1 + _filling);
                float powerMaxSquar = (float)(_info.maxPower * _info.maxPower);

                float slowdown = 1 - _info.slowdown * powerSquar / powerMaxSquar;

                slowdown = slowdown < 0.1f ? 0.1f : slowdown;

                _playerInfo.SpeedFraud(_info.key, slowdown);

                if (_info.power <= 0)
                {
                    _playerInfo.SpeedFraud(_info.key, 1);
                }
            }
        }

        public virtual void DoDamage()
        {
            Debug.Log("DoDamage");

            _playerInfo.MakeDamage(_info.damage * _info.power, _info.multiplierHitPoint, _info.multiplierShieldPoint);
        }

        public virtual void DestroyModificator()
        {
            DoSlowDown(); 
            _playerInfo.SpeedFraud(_info.key, 1);
            Destroy(_element.gameObject);
            Destroy(gameObject);
        }
    }

    [Serializable]
    public class ModificatorInfo
    {
        public int tir;
        //public string type;
        public string key;
        public float time;
        public int maxPower;
        public int power;
        public float damageFull;
        public float damage;
        public float multiplierHitPoint;
        public float multiplierShieldPoint;
        public float slowdown;
        public List<string> additionalEffects = new List<string>();
        public List<string> hidingSpheres = new List<string>();

        public ModificatorInfo()
        {

        }

        public ModificatorInfo(string key, int power)
        {

            if(MagicConst.MAGICS_MODIFICATOR_BY_KEY.TryGetValue(key, out ModificatorInfo info))
            {
                /*if (power > info.maxPower)
                {
                    power = info.maxPower;
                }*/

                this.tir = info.tir;
                //this.type = info.type;
                this.key = key;
                this.time = info.time;
                this.maxPower = info.maxPower;
                this.power = power;
                this.damageFull = info.damageFull;
                this.damage = info.damage;
                this.multiplierHitPoint = info.multiplierHitPoint;
                this.multiplierShieldPoint = info.multiplierShieldPoint;
                this.slowdown = info.slowdown;
                this.additionalEffects = info.additionalEffects;
                this.hidingSpheres = info.hidingSpheres;
                
            }
            else
            {
                this.key = MagicConst.ERROR_KEY;
                this.power = 0;
            }
        }

        public ModificatorInfo(int tir, string key, float time, int maxPower,
            float damageFull, float multiplierHitPoint, float multiplierShieldPoint, float slowdown,
            List<string> additionalEffects, List<string> hidingSpheres)
        {
            this.tir = tir;
           // this.type = type;
            this.key = key;
            this.time = time;
            this.power = 1;
            this.maxPower = maxPower;
            this.damageFull = damageFull;

            int powerFactarial = 0;

            for(int i = 1; i< maxPower + 1; i++)
            {
                powerFactarial += i;
            }

            this.damage = (damageFull/ powerFactarial) / time;
            this.multiplierHitPoint = multiplierHitPoint;
            this.multiplierShieldPoint = multiplierShieldPoint;
            this.slowdown = slowdown;
            this.additionalEffects = additionalEffects;
            this.hidingSpheres = hidingSpheres;
        }
        public ModificatorInfo(int tir, string key, float time, int maxPower,
            float damageFull, float multiplierHitPoint, float multiplierShieldPoint, float slowdown,
            List<string> additionalEffects)
        {
            this.tir = tir;
            //this.type = type;
            this.key = key;
            this.time = time;
            this.power = 1;
            this.maxPower = maxPower;
            this.damageFull = damageFull;

            int powerFactarial = 0;

            for (int i = 1; i < maxPower + 1; i++)
            {
                powerFactarial += i;
            }

            this.damage = (damageFull / powerFactarial) / time;
            this.multiplierHitPoint = multiplierHitPoint;
            this.multiplierShieldPoint = multiplierShieldPoint;
            this.slowdown = slowdown;
            this.additionalEffects = additionalEffects;
            this.hidingSpheres = new List<string>();
        }
    }
    //tir, key, time, power, maxPower, damageFull, multiplierHitPoint, multiplierShieldPoint, slowdown, unArmor,
    //List<string> additionalEffects, type, List<string> meltCastSequences
}
