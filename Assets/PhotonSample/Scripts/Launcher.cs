using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace com.cyraxchel.pun {

    public class Launcher : MonoBehaviourPunCallbacks {

        #region Private Serializable Fields
        [Tooltip("Max number players in room"), SerializeField]
        private byte maxPlayersPerRoom = 4;

        [SerializeField]
        private GameObject controlPanel;
        [SerializeField]
        private GameObject progressLabel;

        [SerializeField]
        private string sceneName = "chardemo";
        
        #endregion

        #region Private Fields
        /// <summary>
        /// This client's version number
        /// </summary>
        string gameVersion = "1";
        bool isConnecting;
        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster() {
            Debug.Log("PUN On connected to master called by PUN");
            if(isConnecting) {
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
        }

        public override void OnDisconnected(DisconnectCause cause) {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            isConnecting = false;
            Debug.LogWarningFormat("On disconnected by reason {0}", cause);

        }

        public override void OnJoinRandomFailed(short returnCode, string message) {
            Debug.Log("Join random room failed. Create new room");
            PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom() {
            Debug.Log("Now client in room");
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1) {
                Debug.Log($"We load ther {sceneName}");
                PhotonNetwork.LoadLevel(sceneName);
            }
        }


        #endregion

        private void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        void Start() {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        public void Connect() {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            if (PhotonNetwork.IsConnected) {
                PhotonNetwork.JoinRandomRoom();
            } else {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        // Update is called once per frame
        void Update() {

        }
    }
}