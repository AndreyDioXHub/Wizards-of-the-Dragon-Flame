using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.czeeep.network.menu {
    public interface IConnection {
        public void ConnectedToPun();
        public void ConnectedToFriendRoom(string _roomname);
        public void ConnectedToButtleRoom();
        public void RegisrenInLobbyManager();
    }
}