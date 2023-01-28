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
        #endregion

        #region Private Fields
        /// <summary>
        /// This client's version number
        /// </summary>
        string gameVersion = "1";

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster() {
            Debug.Log("PUN On connected to master called by PUN");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause) {
            Debug.LogWarningFormat("On disconnected by reason {0}", cause);

        }

        public override void OnJoinRandomFailed(short returnCode, string message) {
            Debug.Log("Join random room failed. Create new room");
            PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom() {
            Debug.Log("Now client in room");
        }


        #endregion

        private void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        void Start() {
            //Connect();
        }

        public void Connect() {
            if(PhotonNetwork.IsConnected) {
                PhotonNetwork.JoinRandomRoom();
            } else {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        // Update is called once per frame
        void Update() {

        }
    }
}