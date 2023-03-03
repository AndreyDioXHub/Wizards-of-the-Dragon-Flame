using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using com.czeeep.spell.magicmodel;
using com.czeeep.spell.staffmodel;

namespace com.czeeep.network.player 
{
    public class PlayerNetwork : MonoBehaviourPunCallbacks, IPunObservable 
    {

        public static GameObject LocalPlayerInstance;
        public static PlayerInfo Info;
        public Transform ModelBackPackMagic { get => _modelBackPackMagic; }
        public Transform ModelBackPackModificator { get => _modelBackPackModificator; }
        public Transform ModelBackPackSphere { get => _modelBackPackSphere; }

        [SerializeField]
        private CharacterController _character;
        [SerializeField]
        private PlayerInfo _info;
        [SerializeField]
        private Transform _modelBackPackMagic;
        [SerializeField]
        private Transform _modelBackPackModificator;
        [SerializeField]
        private Transform _modelBackPackSphere;


        [SerializeField]
        private Vector3 _forward;
        [SerializeField]
        private List<Vector3> _forwards = new List<Vector3>();
        [SerializeField]
        private int _forwardIndex = 0;

        /*[SerializeField]
        private Staff _staff;*/

        [SerializeField]
         private Transform _point;

        /*[SerializeField]
        private float _mouseSensitivity = 100f;
        [SerializeField]
        private float _speed;*/
        [SerializeField]
        private LayerMask _ignoreLayers;

        private float _xRotation = 0f;
        private float _angleTrashHold = 1f;
        private float _positionTrashHold = 0.5f;

        [SerializeField]
        private float _interactableDistance = 2;
        private bool _foundInteractable = false;

        private bool _move;
        //private bool _rotate;
        [SerializeField]
        private bool _isGrounded;

        private Quaternion _targetRotation;
        private Vector3 _pointposition;
        private Vector3 _transformposition;
        private Vector3 _targetDirection;
        private Vector3 _targetDirectionNormalize;
        private Vector3 _moveForwardVector;

        [SerializeField]
        private float _maxSpeed;
        [SerializeField]
        private float _forwardVelocity;
        [SerializeField]
        private Vector3 _velocity;

        //private Vector3 _pointA;
        //private Vector3 _pointB;
        private float _dist;
        //private float _deltaAngle;
        //private float _rotationKeel;
        private Vector3 _gravity = Vector3.zero;
        //private float _gravityDamage = 0;

        [Tooltip("The current health of our player")]
        //public float Health = 1f;
        //public GameObject PlayerUiPrefab;
        //bool IsFiring;

        #region Monobehaviour callbacks

        
        void Awake() 
        {
            /*if(beams == null) {
                Debug.LogError("Missing beams!");
            } else {
                beams.SetActive(false);
            }/**/

            if(photonView.IsMine) 
            {
                LocalPlayerInstance = gameObject;
                Info = _info;
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start() 
        {
            if (photonView.IsMine)
            {
                FollowCamera.Instance.Init(transform);
                StaffModel.Instance.Init(this);
                MagicModel.Instance.Init(this);
                _info.SetHPView(HitPointView.Instance);

                for(int i=0; i< _forwards.Count - 1; i++)
                {
                    _forwards[i] = transform.forward;
                }
            }
            else
            {
                Destroy(_character);
            }

        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode) 
        {
            if(!Physics.Raycast(transform.position, Vector3.down, 5f)) 
            {
                transform.position = Vector3.up * 5f;
            }

        }

        public override void OnDisable() 
        {
            base.OnDisable();
            //SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        // Update is called once per frame
        void Update() 
        {
            if (!photonView.IsMine)
            {
                return;
            }

            _forwards[_forwardIndex] = transform.forward;
            _forwardIndex++;

            if(_forwardIndex > _forwards.Count - 1)
            {
                _forwardIndex = 0;
            }

            int weight = 0;
            int weightTotal = 0;
            foreach (var f in _forwards)
            {

                weight++;
                weightTotal += weight;

                _forward += f * weight;
                Debug.DrawLine(transform.position, transform.position + f, Color.green);
            }

            _forward = _forward / weightTotal;

            Debug.DrawLine(transform.position, transform.position + _forward, Color.red);

            _isGrounded = _character.isGrounded;

            if (_isGrounded)
            {
                _gravity = Vector3.zero;
            }
            else
            {
                _gravity.y += -9.8f * Time.deltaTime;
                _character.Move(_gravity * Time.deltaTime);
            }

            if (_info.IsStuned)
            {
                return;
            }

            var point = _point.position;
            _move = true;
            _pointposition = point;
            _pointposition.y = 0;
            _transformposition = transform.position;
            _transformposition.y = 0;
            _targetDirection = point - transform.position;
            _targetDirection.y = 0.00F;

            transform.rotation = Quaternion.LookRotation(_targetDirection);

            if (_targetDirection.x * _targetDirection.x < _positionTrashHold)
            {
                _targetDirection.x = 0;
            }
            if (_targetDirection.z * _targetDirection.z < _positionTrashHold)
            {
                _targetDirection.z = 0;
            }

            _targetDirectionNormalize = _targetDirection.normalized;


            _moveForwardVector = _forward;// transform.forward;

            if (Input.GetMouseButton(1) )
            {
                _forwardVelocity += _info.Acceleration * Time.deltaTime;
                _forwardVelocity = Math.Min(_forwardVelocity, _info.Speed);
            }
            else
            {
                _forwardVelocity -= _info.Acceleration * Time.deltaTime;
                _forwardVelocity = Math.Max(_forwardVelocity, 0);
            }

            /*
            if (Input.GetKey(KeyCode.S))
            {
                _moveForwardVector = -transform.forward;
            }*/



            _velocity = _moveForwardVector * _forwardVelocity;


            if (_move)
            {
                _character.Move(_velocity * Time.deltaTime);

                if (_dist > _positionTrashHold)
                {
                    _transformposition = transform.position;
                    _transformposition.y = 0;
                    _dist = Vector3.Distance(_pointposition, _transformposition);
                }
                else
                {
                    _move = false;
                }
            }

            if (Input.GetMouseButton(0))
            {
                StaffModel.Instance.Shoot(CastDirection.forward);
            }

            if (Input.GetKey(KeyCode.Tab))
            {
                StaffModel.Instance.Shoot(CastDirection.self);
            }

            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Tab))
            {
                StaffModel.Instance.ShootStop();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                //StaffModel.Instance.ShootStop();
                MagicModel.Instance.ReturnAllSphereToInventory();
            }

            if (!Input.GetMouseButtonDown(1))
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.water.ToString());
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.life.ToString());
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.shield.ToString());
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.freeze.ToString());
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.razor.ToString());
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.dark.ToString());
                }
                if (Input.GetKeyDown(KeyCode.B))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.earth.ToString());
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.fire.ToString());
                }
            }
            

        }




        #endregion

        #region Custom
        /*
        private void ProcessInputs() 
        {
            if (Input.GetButtonDown("Fire1")) {
                if(!IsFiring) {
                    IsFiring = true;
                }
            }
            if (Input.GetButtonUp("Fire1")) {
                if (IsFiring) {
                    IsFiring = false;
                }
            }
        }*/
       

        #endregion
        
        #region IpunObservable implementation
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
        {
            if(stream.IsWriting) 
            {
                //We own this player: send the others out data
                /*stream.SendNext(IsFiring);
                stream.SendNext(Health);*/
            } 
            else 
            {
                /*this.IsFiring = (bool)stream.ReceiveNext();
                this.Health = (float)stream.ReceiveNext();*/
            }
        }

        #endregion
    }
}