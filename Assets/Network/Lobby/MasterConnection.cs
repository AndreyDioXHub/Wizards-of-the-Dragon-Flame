using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

namespace com.czeeep.network.menu {
    public class MasterConnection : MonoBehaviour, IConnection{
        #region Events

        public UnityEvent<string> OnFriendRoomCreated;
        public UnityEvent<string> OnFriendRoomNameChanged;
        public UnityEvent BeginFriendRoomCreation;

        #endregion


        #region IConnection


        public void ConnectedToButtleRoom() {
           // throw new System.NotImplementedException();
        }

        public void ConnectedToFriendRoom(string _roomname) {
            OnFriendRoomNameChanged?.Invoke(_roomname);
            OnFriendRoomCreated?.Invoke(_roomname);
            Debug.Log($"Connected to room {_roomname}");
            //TODO
        }

        public void ConnectedToPun() {
            //throw new System.NotImplementedException();
        }

        public void RegisrenInLobbyManager() {
            //throw new System.NotImplementedException();
        }

        #endregion

        private void OnEnable() {
            PlayersLobbyManager.Instance.RegisterUserConnection(this);
        }
        private void OnDisable() {
            PlayersLobbyManager.Instance.UnregisterUserConnection(this);
        }

        public void CreateFriendRoom() {
            OnFriendRoomNameChanged?.Invoke("");
            BeginFriendRoomCreation?.Invoke();
            string rname = PlayersLobbyManager.GenerateRoomName();
            PlayersLobbyManager.Instance.ConnectToFriendRoom(rname);
        }

    }
}