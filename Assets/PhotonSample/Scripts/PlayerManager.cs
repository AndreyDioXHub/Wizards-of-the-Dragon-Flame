using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace com.cyraxchel.pun {
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable {

        [SerializeField]
        GameObject beams;

        [Tooltip("The current health of our player")]
        public float Health = 1f;
        public static GameObject LocalPlayerInstance;
        public GameObject PlayerUiPrefab;
        bool IsFiring;

        #region Monobehaviour callbacks

        
        void Awake() {
            if(beams == null) {
                Debug.LogError("Missing beams!");
            } else {
                beams.SetActive(false);
            }
            if(photonView.IsMine) {
                LocalPlayerInstance = gameObject;
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            CameraWork cameraWork = GetComponent<CameraWork>();
            if(cameraWork != null) {
                if(photonView.IsMine) {
                    cameraWork.OnStartFollowing();
                    //this.name = "Local Player";
                }
            } else {
                Debug.LogError("Missing CameraWork comnotent on ", this);
            }
            SceneManager.sceneLoaded += OnSceneLoaded;
            if(PlayerUiPrefab != null) {
                GameObject _uiGo = Instantiate(PlayerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            } else {
                Debug.LogWarning("Missing PlayerUiPrefab",this);
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode) {
            if(!Physics.Raycast(transform.position, Vector3.down, 5f)) {
                transform.position = Vector3.up * 5f;
            }
            GameObject _uiGO = Instantiate(PlayerUiPrefab);
            _uiGO.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        public override void OnDisable() {
            base.OnDisable();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        // Update is called once per frame
        void Update() {
            if (photonView.IsMine) {
                ProcessInputs();
            }
                if (Health <= 0) {
                    GameManager.Instance.LeaveRoom();
                }
                if (beams != null && IsFiring != beams.activeInHierarchy) {
                    beams.SetActive(IsFiring);
                }
            
        }

        private void OnTriggerEnter(Collider other) {
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

        internal void SetFireButton(Button fireButton) {
            if(fireButton != null) {
                ButtonFire bfire = fireButton.GetComponent<ButtonFire>();
                bfire.OnPointerPush.AddListener(()=> { IsFiring = true; });
                bfire.OnPointerUpped.AddListener(()=> { IsFiring = false; });
            }
        }

        private void OnTriggerStay(Collider other) {
            Debug.Log("PlayerManager OntriggerStay");
            if(!photonView.IsMine) {
                return;
            }
            if(other.name.Contains("Beam")) {
                return;
            }
            Health -= 0.1f * Time.deltaTime;
        }

        

        #endregion

        #region Custom

        private void ProcessInputs() {
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
        }
       

        #endregion
        
        #region IpunObservable implementation
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
            if(stream.IsWriting) {
                //We own this player: send the others out data
                stream.SendNext(IsFiring);
                stream.SendNext(Health);
            } else {
                this.IsFiring = (bool)stream.ReceiveNext();
                this.Health = (float)stream.ReceiveNext();
            }
        }

        #endregion
    }
}