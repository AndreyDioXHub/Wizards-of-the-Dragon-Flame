using com.czeeep.spell.staffmodel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.czeeep.spell.magic
{
    public class Magic : MonoBehaviour
    {
        [SerializeField]
        protected MagicInfo _magicInfo;

        // Start is called before the first frame update
        void Start()
        {

            //Debug.Log("StaffModel Shoot Magic");
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log($"cast {_magicInfo.name}: {_magicInfo.power}");
        }

        public void Init()
        {
        }

        public void UpdateInfo(MagicInfo magicInfo)
        {
            _magicInfo = magicInfo;
            name = $"{"{"}\"key\": \"{_magicInfo.key}\", \"power\": {(int)_magicInfo.power}, \"direction\": {(int)_magicInfo.direction}{"}"}";
        }

        public void DestroyMagic()
        {
            Destroy(gameObject);
        }
    }

    [Serializable]
    public class MagicInfo
    {
        public string key;
        public int power;
        public CastDirection direction;

        public MagicInfo(string key, CastDirection direction, int power)
        {
            this.key = key;
            this.direction = direction;
            this.power = power;
        }

    }


}