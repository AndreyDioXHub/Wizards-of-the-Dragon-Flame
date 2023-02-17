using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace com.czeeep.network {

    public class SphereManager : MonoBehaviour {
        #region Constants

        public const string CreateSphereAction = "CreateSphere";

        #endregion

        #region PrivateFields

        [SerializeField]
        GameObject spherePrefab;

        List<GameObject> _spheres = new List<GameObject>();

        [SerializeField]
        SphereConfig _config;

        #endregion

        #region Piblic Fields

        public PhotonView photonView { get; set; }


        #endregion

        #region MonoBehaviour callback
        private void Awake() {
            if (_config == null) {
                Debug.Log("Create config: <b>Later load it from settings</b>");
                _config = new SphereConfig();
            }
            photonView = PhotonView.Get(this);
        }
        #endregion

        #region Public Methods


        public void CreateSpheres() {
            if(PhotonNetwork.IsMasterClient) {
                //Create spheres
                for (int i = 0; i < _config.MaxSpheres; i++) {
                    CreateSphere(SphereConfig.GenerateRandomPosition(), Quaternion.identity);
                }
            }
        }

        [PunRPC]
        protected void CreateSphere(Vector3 pos, Quaternion rotation, int elementType = -1) {
            GameObject _sphere = Instantiate(spherePrefab, null);
            _sphere.transform.position = pos;
            _sphere.transform.rotation = rotation;
            _sphere.GetComponent<SphereWorld>().Init((SpheresElements)elementType, 5);  //TODO Add count by logic!
            _spheres.Add(_sphere);
            //TODO Addlink to Manager

            if(photonView != null) {
                photonView.RPC(CreateSphereAction, RpcTarget.Others, pos, rotation, elementType);
            }
        }
        #endregion

        [Serializable]
        public class SphereConfig {

            [Tooltip("Зона распределения сфер")]
            public Rect rect;
            [Tooltip("Максимальное количество сфер")]
            public int MaxSpheres = 20;

            static System.Random random = null;

            public static Vector3 GenerateRandomPosition() {
                if(random == null) {
                    random = new System.Random(100);
                }
                Vector3 pos = new Vector3(random.Next(), 0, random.Next());
                return pos;
            }
        }
    }
}