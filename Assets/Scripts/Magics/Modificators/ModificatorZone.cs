using com.czeeep.spell.magicmodel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.czeeep.spell.biom {
    public class ModificatorZone : MonoBehaviour {

        public static readonly string Tag = "biom";     //tag name in editor

        [SerializeField]
        private Tick _tick;

        [SerializeField, Tooltip("Config type for this biom")]
        private Bioms biomType = Bioms.none;
        private string _element;
        [SerializeField]
        private int _power = 1;

        [SerializeField]
        private Collider _other;

        public Bioms ZoneBiom { get => biomType; }

        // Start is called before the first frame update
        void Awake() {
            _element = biomType.ToString();
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
    }
}