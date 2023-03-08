using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using TMPro;

namespace com.czeeep.network.menu {

    public class PlayerMenuManager : MonoBehaviourPunCallbacks {
        [SerializeField]
        GameObject elementMenuPrefab;

        GameObject menuInList;

        // Start is called before the first frame update
        void Start() {
            CreateListItem();
        }
        private void OnDestroy() {
            DestroyListItem();
        }

        private void CreateListItem() {
            menuInList = Instantiate(elementMenuPrefab, FriendsList.Instance.UserListContainer, false);
            //TODO Apply info about user
            TextMeshProUGUI _text = menuInList.GetComponentInChildren<TextMeshProUGUI>();
            if (_text != null) {
                _text.text = PhotonNetwork.LocalPlayer.NickName;
            }
        }


        private void DestroyListItem() {
            if(menuInList != null) {
                Destroy(menuInList);
            }
        }

        // Update is called once per frame
        void Update() {

        }
    }
}