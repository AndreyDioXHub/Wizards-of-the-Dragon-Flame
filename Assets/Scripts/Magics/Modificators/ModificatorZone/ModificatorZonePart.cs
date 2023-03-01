using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.czeeep.spell.biom
{
    public class ModificatorZonePart : MonoBehaviour
    {
        public Collider Player { get => _player; }
        [SerializeField]
        private ModificatorZone _zone;
        [SerializeField]
        private Collider _player;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            //_tick.UpdateTick();

            if (other.tag == "Player")
            {
                _player = other;
                _zone.CheckTriggerEnter();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _player = null;
            _zone.CheckTriggerExit();
        }
    }

}
