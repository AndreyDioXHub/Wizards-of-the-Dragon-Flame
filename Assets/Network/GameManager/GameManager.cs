using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using com.czeeep.network.player;

namespace com.czeeep.network {



    public class GameManager : MonoBehaviourPunCallbacks {

        #region Public Fields

        const string UpdateMethodCall = "MasterUpdateSpheres";

        public static GameManager Instance;
        public GameObject playerPrefab;

        public Joystick joystick;
        public Button fireButton;
        public SphereManager sphereManager;

        #endregion

        #region Photon Callbacks
        public override void OnLeftRoom() {
            SceneManager.LoadScene(0);
        }
        #endregion

        #region Public Methods
        [ContextMenu("Leave Room")]
        public void LeaveRoom() {
            PhotonNetwork.LeaveRoom();
        }
        #endregion


        #region Private Methods

        void LoadArena() {
            if(!PhotonNetwork.IsMasterClient) {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
                return;
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        }

        #endregion


        #region Photon Callbacks

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer) {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName);
            if(PhotonNetwork.IsMasterClient) {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
                //LoadArena();
                //TODO Generate List of shperes and set it to Room properties
            }
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", otherPlayer.NickName);
            if(PhotonNetwork.IsMasterClient) {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            }
        }

        public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged) {
            base.OnRoomPropertiesUpdate(propertiesThatChanged);
            //Update spheres elements
            //PhotonNetwork.CurrentRoom.SetCustomProperties()
        }

        #endregion


        #region MonoBehaviour callbacks


        // Start is called before the first frame update
        void Start() {
            Instance = this;
            if(playerPrefab == null) {
                Debug.LogError("Missing player prefab");
            } else {
                if(PlayerNetwork.LocalPlayerInstance == null) {
                    Debug.LogFormat("We are Instantinating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    var GO = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.up * 5f, Quaternion.identity, 0);
                    //GO.GetComponent<PlayerAnimationManager>().SetControl(joystick);
                    //GO.GetComponent<PlayerManager>().SetFireButton(fireButton);
                } else {
                    //PlayerManager.LocalPlayerInstance.GetComponent<PlayerAnimationManager>().SetControl(joystick);
                    //PlayerManager.LocalPlayerInstance.GetComponent<PlayerManager>().SetFireButton(fireButton);
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }
            if(PhotonNetwork.IsMasterClient) {
                sphereManager.CreateSpheres();
            } else {
                //TODO Load from customProperties
                var hashtable = PhotonNetwork.CurrentRoom.CustomProperties;
                sphereManager.CreateSpheres(hashtable);
                //var photonView = PhotonView.Get(this);
                //photonView.RPC("MasterUpdateSpheres", RpcTarget.MasterClient);
            }
            
        }

        [PunRPC]
        public void MasterUpdateSpheres() {
            sphereManager.SyncSpheres();
        }


        // Update is called once per frame
        void Update() {

        }

        #endregion
    }
}