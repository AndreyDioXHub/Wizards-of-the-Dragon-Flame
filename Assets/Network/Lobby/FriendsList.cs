using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using TMPro;

namespace com.czeeep.network.menu {
    public class FriendsList : MonoBehaviourPunCallbacks {

        [SerializeField]
        GameObject UserfieldPrefab;
        [SerializeField]
        GameObject ListElementPrefab;
        [SerializeField]
        Transform container;

        Dictionary<int, GameObject> ListOfUser = new Dictionary<int, GameObject>();

        public Transform UserListContainer { get {
                return container;
            } }

        public static FriendsList Instance;
        // Start is called before the first frame update
        void Awake() {
            Instance = this;
        }

        // Update is called once per frame
        void Update() {

        }

        public void RoomCreated(string roomName) {
            //Add to list
            //GameObject user = PhotonNetwork.Instantiate(UserfieldPrefab.name, Vector3.zero, Quaternion.identity);
            //TODO May be replace to PhotonNetwork.PlayerList
        }

        #region PUN CALLBACKS

        public override void OnJoinedRoom() {
            base.OnJoinedRoom();
            RoomCreated(PhotonNetwork.CurrentRoom.Name);

            //Load list of users
            Photon.Realtime.Player[] _players = PhotonNetwork.PlayerList;
            CreateStartList(_players);
        }

        private void CreateStartList(Photon.Realtime.Player[] _players) {
            for (int i = 0; i < _players.Length; i++) {
                CreateListItem(_players[i]);
            }
        }

        private void CreateListItem(Photon.Realtime.Player player) {
            var go = Instantiate(ListElementPrefab, UserListContainer, false);
            //TODO Apply info about user
            TextMeshProUGUI _text = go.GetComponentInChildren<TextMeshProUGUI>();
            if (_text != null) {
                _text.text = player.NickName;
            }
            if(ListOfUser.ContainsKey(player.ActorNumber)) {
                //Remove
                RemovePlayerFromList(player.ActorNumber);
            }
            ListOfUser.Add(player.ActorNumber, go);
        }

        private void RemovePlayerFromList(int actorNumber) {
            GameObject _listelement;
            if(ListOfUser.TryGetValue(actorNumber, out _listelement)) {
                ListOfUser.Remove(actorNumber);
                Destroy(_listelement);
            }
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer) {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log(newPlayer.NickName);
            CreateListItem(newPlayer);
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) {
            base.OnPlayerLeftRoom(otherPlayer);
            RemovePlayerFromList(otherPlayer.ActorNumber);
        }

        #endregion
    }
}