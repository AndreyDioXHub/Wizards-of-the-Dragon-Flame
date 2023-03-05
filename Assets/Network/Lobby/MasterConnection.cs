using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace com.czeeep.network {
    public class MasterConnection : MonoBehaviour, IConnection{

        #region IConnection

        
        public void ConnectedToButtleRoom() {
           // throw new System.NotImplementedException();
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

        private void OnEnable() {
            PlayersLobbyManager.Instance.RegisterUserConnection(this);
        }
        private void OnDisable() {
            PlayersLobbyManager.Instance.UnregisterUserConnection(this);
        }

        public void CreateFriendRoom() {
            
        }
    }
}