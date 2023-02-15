using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace com.cyraxchel.pun {



    public class GameManager : MonoBehaviourPunCallbacks {

        #region Public Fields

        public static GameManager Instance;
        public GameObject playerPrefab;

        public Joystick joystick;
        public Button fireButton;

        #endregion

        #region Photon Callbacks
        public override void OnLeftRoom() {
            SceneManager.LoadScene(0);
        }
        #endregion

        #region Public Methods
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
                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", otherPlayer.NickName);
            if(PhotonNetwork.IsMasterClient) {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            }
        }

        #endregion

        // Start is called before the first frame update
        void Start() {
            Instance = this;
            if(playerPrefab == null) {
                Debug.LogError("Missing player prefab");
            } else {
                if(PlayerManager.LocalPlayerInstance == null) {
                    Debug.LogFormat("We are Instantinating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    var GO = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.up * 5f, Quaternion.identity, 0);
                    GO.GetComponent<PlayerAnimationManager>().SetControl(joystick);
                    GO.GetComponent<PlayerManager>().SetFireButton(fireButton);
                } else {
                    PlayerManager.LocalPlayerInstance.GetComponent<PlayerAnimationManager>().SetControl(joystick);
                    PlayerManager.LocalPlayerInstance.GetComponent<PlayerManager>().SetFireButton(fireButton);
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
                
            }
        }

        // Update is called once per frame
        void Update() {

        }
    }
}