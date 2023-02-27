using com.czeeep.spell.magicmodel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.czeeep.spell.biom {
    public class ModificatorZone : MonoBehaviour {

        public static readonly string Tag = "biom";     //tag name in editor
        public static readonly int MIN_VALUE = 1;
        public static readonly int MAX_VALUE = 10;

        [SerializeField]
        private Tick _tick;

        [SerializeField, Tooltip("Config type for this biom")]
        private Bioms biomType = Bioms.none;
        private string _element;
        [SerializeField]
        private int _power = 1;

        [SerializeField]
        private Collider _other;

        int increaseValue = 0;
        int decreaseValue = 0;


        public Bioms ZoneBiom { get => biomType; }

        // Start is called before the first frame update
        void Awake() {
            _element = biomType.ToString();
            //Get my increase and decrease modification
            GetMyModificators();
        }

        private void GetMyModificators() {
            if(MagicConst.IncreaseSphereCount.ContainsKey(biomType)) {
                MagicConst.IncreaseSphereCount.TryGetValue(biomType, out increaseValue);
            }
            if (MagicConst.DecreaseSphereCount.ContainsKey(biomType)) {
                MagicConst.DecreaseSphereCount.TryGetValue(biomType, out decreaseValue);
            }
        }

        public void UpdateInfo(string element, int power) {
            _element = element;
            _power = power;
        }

        public void DestroyZone() {
            Destroy(gameObject.transform.parent.gameObject);
        }

        private void OnTriggerEnter(Collider other) {
            _tick.UpdateTick();
            _other = other;
            AddModificator();
        }
        private void OnTriggerExit(Collider other) {
            _other = null;
        }

        public void AddModificator() {
            if (_other != null) {
                if (_other.tag == "Player") {
                    //Debug.Log("UpdateTick AddModificator ");
                    MagicModel.Instance.AddModificator(_element, _power);
                }
            }
        }

        public int GetSphereCountByType(SpheresElements spheretype, int count) {
            if(((int)spheretype & increaseValue) > 0) {
                count = MAX_VALUE;
            }
            if (((int)spheretype & decreaseValue) > 0) {
                count = MIN_VALUE;
            }
            return count;
        }
    }
}