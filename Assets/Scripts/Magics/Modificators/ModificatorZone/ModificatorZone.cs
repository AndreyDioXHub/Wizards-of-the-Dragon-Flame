using com.czeeep.spell.magicmodel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.czeeep.spell.biom 
{
    public class ModificatorZone : MonoBehaviour
    {
        public UnityEvent OnPlayerColliding = new UnityEvent();

        public static readonly string Tag = "biom";     //tag name in editor
        public static readonly int MIN_VALUE = 1;
        public static readonly int MAX_VALUE = 10;

        [SerializeField]
        protected List<ModificatorZonePart> _parts = new List<ModificatorZonePart>();

        [SerializeField]
        private Tick _tick;

        [SerializeField]
        protected Transform _zoneCenter;
       /* [SerializeField]
        private Transform _zoneEnd;*/

        [SerializeField]
        private Vector3 _dirToPlayer;

        [SerializeField, Tooltip("Config type for this biom")]
        private Bioms biomType = Bioms.none;

        [SerializeField]
        private string _element;

        [SerializeField]
        private int _power = 1;

        [SerializeField]
        protected Collider _player;

        int increaseValue = 0;
        int decreaseValue = 0;



        public Bioms ZoneBiom { get => biomType; }

        // Start is called before the first frame update
        void Awake() 
        {
            if (string.IsNullOrEmpty(_element))
            {
                _element = biomType.ToString();
            }

            _zoneCenter = transform;

            //Get my increase and decrease modification
            GetMyModificators();
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        [ContextMenu("Collect Modificator Zone Part")]
        public void CollectModificatorZonePart()
        {
            var parts = gameObject.GetComponentsInChildren<ModificatorZonePart>();

            foreach(var p in parts)
            {
                _parts.Add(p);
            }
        }

        [ContextMenu("Add New Part")]
        public void AddNewPart()
        {   
            if(_parts.Count > 0)
            {
                var go = Instantiate(_parts[_parts.Count - 1].gameObject, transform);
                go.name = $"ZonePart ({_parts.Count})";
                var part = go.GetComponent<ModificatorZonePart>();
                _parts.Add(part);
            }
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

        public void DestroyZone() 
        {
            Destroy(gameObject);
        }

        public void CheckTriggerEnter()
        {
            foreach(var part in _parts)
            {
                if(part.Player != null)
                {
                    _player = part.Player;
                }
            }

            if(_player != null)
            {
                _tick.UpdateTick();

                //AddModificator();
                OnPlayerColliding?.Invoke();
            }
        }
        
        public void CheckTriggerExit()
        {
            Collider player = null;

            foreach (var part in _parts)
            {
                if (part.Player != null)
                {
                    player = part.Player;
                }
            }

            _player = player;
        }


        /*
        private void OnTriggerEnter(Collider other) 
        {

            if (other.tag == "Player")
            {
                _tick.UpdateTick();

                _player = other;

                AddModificator();

                if (_destroyAfterColliding)
                {
                    DestroyZone();
                }
            }

        }

        private void OnTriggerExit(Collider other) 
        {
            _player = null;
        }*/


        public void AddModificator() 
        {
            if (_player != null) 
            {
                if (_zoneCenter != null)
                {
                    _dirToPlayer = DirToPlayer();// transform.forward;
                    //_player.transform.position - _zoneCenter.position;
                    //_dirToPlayer = _dirToPlayer.normalized;

                }
                else
                {
                    _dirToPlayer = Vector3.forward;
                }

                //Debug.Log("UpdateTick AddModificator ");
                MagicModel.Instance.AddModificator(_element, _power, _dirToPlayer);
            }
        }

        public virtual Vector3 DirToPlayer()
        {
            Vector3 dir = transform.forward;
            return dir;
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