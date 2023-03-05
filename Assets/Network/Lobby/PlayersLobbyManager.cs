using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartRandomAlphanumericGenerator;
using System;
using Photon.Pun;
using Photon.Realtime;

namespace com.czeeep.network {

    public class PlayersLobbyManager :  MonoBehaviourPunCallbacks {
        public static PlayersLobbyManager Instance;

        #region Inspector fields

        [SerializeField]
        string roomName;

        [Tooltip("Max number players in team"), SerializeField]
        private byte maxPlayersPerTeam = 4;

        [Tooltip("Max number players in match"), SerializeField]
        private byte maxPlayersPerMathc = 30;
        #endregion

        #region Private fields

        string gameVersion = "1";
        bool isConnecting = false;
        IConnection _playerConnection;

        #endregion

        #region MONOBEHAVIOUR CALLBACKS

        private void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
            Instance = this;
        }


        void Start() {
            roomName = GenerateRoomName(6);
        }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster() {
            base.OnConnectedToMaster();
            if(isConnecting && _playerConnection !=null) {
                _playerConnection.ConnectedToPun();
            }
        }

        public override void OnDisconnected(DisconnectCause cause) {
            base.OnDisconnected(cause);
            isConnecting = false;
            
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {
            base.OnJoinRandomFailed(returnCode, message);
            //TODO
        }

        public override void OnJoinRoomFailed(short returnCode, string message) {
            base.OnJoinRoomFailed(returnCode, message);
            //TODO
        }

        public override void OnJoinedRoom() {
            base.OnJoinedRoom();
            //TODO

        }


        #endregion

        #region Public

        public void ConnectToMaster() {
            if(!PhotonNetwork.IsConnected) {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            } 
        }

        public void ConnectToFriendRoom(string _roomName = "") {
            if(string.IsNullOrEmpty(_roomName) ) {
                //Create room
            } else {
                //Try connect to room
            }
        }

        public void ConnectToButtleRoom() {

        }

        public void RegisterUserConnection(IConnection _conn) {
            Debug.Log($"Register connection: {_conn}");
            _playerConnection = _conn;
        }
        public void UnregisterUserConnection(IConnection _connection) {

            if (_connection == _playerConnection) {
                Debug.Log($"UnRegister connection: {_connection}");
                _playerConnection = null;
            }
        }



        #endregion

        #region PUBLIC STATIC
        public static string GenerateRoomName(int CharCount = 6) {
            ISRAGenerator srag = new SRAGenerator();
            srag.UseLowercaseLetters = false;
            srag.UseNumbers = false;
            srag.UseUppercaseLetters = true;
            srag.UseSymbols = false;
            return srag.Generate(CharCount);
        }

        #endregion

    }
}