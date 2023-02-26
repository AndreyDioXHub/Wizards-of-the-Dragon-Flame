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
       /* [SerializeField]
        protected int _damage = 1;*/
        /*[SerializeField]
        protected int _maxPower = 10;*/
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

        public virtual void Init(string key, int power)
        {
            Named();
            _timeActionCur = 0;
            _info = new ModificatorInfo(key, power);
            //_info.power = power;
            _playerInfo = PlayerNetwork.Info;
            //DoDamage();
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
        public int maxPower;
        public int power;/*{ 
            get { return power; }
            set { 
                if (value > maxPower) 
                {
                    value = maxPower; 
                } } 
        }*/
        public float damageFull;
        public float damage;
        public float multiplierHitPoint;
        public float multiplierShieldPoint;
        public float slowdown;
        public List<string> additionalEffects = new List<string>();

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
                this.type = info.type;
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
                
            }
            else
            {
                this.key = MagicConst.ERROR_KEY;
                this.power = 0;
            }
        }

        public ModificatorInfo(int tir, string type, string key, float time, int maxPower,
            float damageFull, float multiplierHitPoint, float multiplierShieldPoint, float slowdown,
            List<string> additionalEffects)
        {
            this.tir = tir;
            this.type = type;
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
        }
    }
    //tir, key, time, power, maxPower, damageFull, multiplierHitPoint, multiplierShieldPoint, slowdown, unArmor,
    //List<string> additionalEffects, type, List<string> meltCastSequences
}
