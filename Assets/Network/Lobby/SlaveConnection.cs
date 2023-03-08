using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.czeeep.network.menu {
    public class SlaveConnection : MonoBehaviour, IConnection {

        #region Private Fields
        string roomName = string.Empty;
        #endregion

        #region IConnection
        public void ConnectedToButtleRoom() {
            //throw new System.NotImplementedException();
        }

        public void ConnectedToFriendRoom(string _roomname) {
            //throw new System.NotImplementedException();
        }

        public void ConnectedToPun() {
            //throw new System.NotImplementedException();
        }

        public void RegisrenInLobbyManager() {
            //throw new System.NotImplementedException();
        }

        #endregion

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        private void OnEnable() {
            PlayersLobbyManager.Instance.RegisterUserConnection(this);
        }
        private void OnDisable() {
            PlayersLobbyManager.Instance.UnregisterUserConnection(this);
        }

        public void ReadRoomName(string _roomname) {
            roomName = _roomname;
        }

        public void ConnectToFriendRoom() {
            if(!string.IsNullOrEmpty(roomName)) {
                PlayersLobbyManager.Instance.ConnectToFriendRoom(roomName);
            }
        }
    }
}