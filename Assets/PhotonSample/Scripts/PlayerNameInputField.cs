using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

namespace com.cyraxchel.pun {

    [RequireComponent(typeof(TMP_InputField))]
    public class PlayerNameInputField : MonoBehaviour {
        #region Private Constants

        const string playerNamePrefKey = "PlayerName";

        #endregion



        // Start is called before the first frame update
        void Start() {
            string defaultName = string.Empty;
            TMP_InputField _inputField = this.GetComponent<TMP_InputField>();
            if(_inputField != null) {
                if(PlayerPrefs.HasKey(playerNamePrefKey)) {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }

            PhotonNetwork.NickName = defaultName;

        }

        #region Public Methods

        public void SetPlayerName(string value) {
            if(string.IsNullOrEmpty(value)) {
                Debug.LogError("Player name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value;
            PlayerPrefs.SetString(playerNamePrefKey, value);
        }

        #endregion
    }
}