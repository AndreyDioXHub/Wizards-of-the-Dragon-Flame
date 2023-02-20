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

namespace com.czeeep.network.player {
    public class PlayerNetwork : MonoBehaviourPunCallbacks, IPunObservable 
    {

        public static GameObject LocalPlayerInstance;
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
        /*[SerializeField]
        private Staff _staff;*/

        /*[SerializeField]
         private Transform _point;*/

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
        private bool _rotate;
        [SerializeField]
        private bool _isGrounded;

        private Quaternion _targetRotation;
        private Vector3 _pointposition;
        private Vector3 _transformposition;
        private Vector3 _targetDirection;
        private Vector3 _targetDirectionNormalize;
        private Vector3 _pointA;
        private Vector3 _pointB;
        private float _dist;
        private float _deltaAngle;
        private float _rotationKeel;
        private float _gravity = 0;

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
            }
            else
            {
                Destroy(_character);
            }



            /*
            CameraWork cameraWork = GetComponent<CameraWork>();

            if(cameraWork != null) 
            {
                if(photonView.IsMine) 
                {
                    cameraWork.OnStartFollowing();
                    //this.name = "Local Player";
                }
            } 
            else 
            {
                Debug.LogError("Missing CameraWork comnotent on ", this);
            }

            SceneManager.sceneLoaded += OnSceneLoaded;

            if(PlayerUiPrefab != null) 
            {
                GameObject _uiGo = Instantiate(PlayerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            else 
            {
                Debug.LogWarning("Missing PlayerUiPrefab",this);
            }*/
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode) 
        {
            if(!Physics.Raycast(transform.position, Vector3.down, 5f)) 
            {
                transform.position = Vector3.up * 5f;
            }

            /*GameObject _uiGO = Instantiate(PlayerUiPrefab);

            _uiGO.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);*/

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

            _isGrounded = _character.isGrounded;

            if (_isGrounded)
            {
                _gravity = 0;
            }
            else
            {
                _gravity = -9.8f;
                _character.Move(transform.up * _gravity * Time.deltaTime);

            }

            if (true)
            {
                RaycastHit hit;
                Ray ray = FollowCamera.Instance.SelfCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, _ignoreLayers))
                {
                    var point = hit.point;
                    _move = true;
                    // _rotate = true;
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

                    _targetDirectionNormalize = _targetDirection.normalized;}
            }

            if (Input.GetMouseButton(0))
            {
                StaffModel.Instance.Shoot(CastDirection.forward);
            }

            if (Input.GetMouseButton(1))
            {
                StaffModel.Instance.Shoot(CastDirection.self);
            }

            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                StaffModel.Instance.ShootStop();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                //StaffModel.Instance.ShootStop();
                MagicModel.Instance.ReturnAllSphereToInventory();
            }

            if (!Input.GetMouseButtonDown(1))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.water.ToString());
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.life.ToString());
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.shield.ToString());
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    //StaffModel.Instance.ShootStop();
                    MagicModel.Instance.AddSpheretoActive(SpheresElements.freze.ToString());
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
            

            if (_rotate)
            {
                if (_deltaAngle > _angleTrashHold * _info.MouseSensitivity)
                {
                    if (_rotationKeel > 0)
                    {
                        _xRotation -= _info.MouseSensitivity * Time.deltaTime;
                    }
                    else
                    {
                        _xRotation += _info.MouseSensitivity * Time.deltaTime;
                    }

                    transform.Rotate(Vector3.up * _xRotation);
                    _deltaAngle = Quaternion.Angle(transform.rotation, _targetRotation);
                }
                else
                {
                    _rotate = false;
                }
            }

            if (_move)
            {
                _character.Move(_info.Speed * _targetDirectionNormalize * Time.deltaTime);

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

            /*if (photonView.IsMine) 
            {
                ProcessInputs();
            }

            if (Health <= 0) 
            {
                GameManager.Instance.LeaveRoom();
            }*/
            /*if (beams != null && IsFiring != beams.activeInHierarchy) {
                beams.SetActive(IsFiring);
            }/**/

        }


        /*
        private void OnTriggerEnter(Collider other) 
        {
            return;
            Debug.Log("PlayerManager OntriggerEnter");
            if (!photonView.IsMine) {
                return;
            }
            Debug.Log("PlayerManager OntriggerEnter IsMine!");
            if (!other.name.Contains("Beam")) {
                return;
            }
            Debug.Log("PlayerManager OntriggerEnter Decrease health!");
            Health -= 0.1f;
        }

        internal void SetFireButton(Button fireButton) 
        {
            if(fireButton != null) 
            {
                ButtonFire bfire = fireButton.GetComponent<ButtonFire>();
                bfire.OnPointerPush.AddListener(()=> { IsFiring = true; });
                bfire.OnPointerUpped.AddListener(()=> { IsFiring = false; });
            }
        }

        private void OnTriggerStay(Collider other) 
        {
            return;
            Debug.Log("PlayerManager OntriggerStay");
            if(!photonView.IsMine) 
            {
                return;
            }
            if(other.name.Contains("Beam")) 
            {
                return;
            }
            Health -= 0.1f * Time.deltaTime;
        }*/



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