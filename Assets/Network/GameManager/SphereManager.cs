using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using com.czeeep.spell.magicmodel;

namespace com.czeeep.network {

    public class SphereManager : MonoBehaviour {
        #region Constants

        public const string CreateSphereAction = "CreateSphere";
        public const string DestroySphereAction = "RemovedExtenral";

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
                SerializaToRoom();
            }
            
        }

        private void SerializaToRoom() {
            //Create properties in room
        }

        [PunRPC]
        protected void CreateSphere(Vector3 pos, Quaternion rotation, int elementType = -1, int indx = -1) {
            GameObject _sphere = Instantiate(spherePrefab, null);
            _sphere.transform.position = pos;
            _sphere.transform.rotation = rotation;
            _spheres.Add(_sphere);
            _sphere.GetComponent<SphereWorld>().Init((SpheresElements)elementType, 5, _spheres.Count-1);
        }

        public void SyncSpheres() {
            if (PhotonNetwork.IsMasterClient) {
                Debug.Log("Start sync");
                for (int i = 0; i < _spheres.Count; i++) {
                    var item = _spheres[i];
                    if(item != null) {
                        var _sphere = item.GetComponent<SphereWorld>();
                        photonView.RPC(CreateSphereAction, RpcTarget.Others, item.transform.position, item.transform.rotation, _sphere.GetElementType(), i);
                    }
                }
            }
        }

        internal void WillDestroyed(int m_index) {
            Debug.Log("<color=red>Destroy sphere local</color>");
            if(PhotonNetwork.IsConnected) {
                photonView.RPC(DestroySphereAction, RpcTarget.Others, m_index);
            }
        }

        [PunRPC]
        public void RemovedExtenral(int m_index) {
            if(m_index > -1 && m_index < _spheres.Count) {
                GameObject _go = _spheres[m_index];
                if(_go != null) {
                    _go.GetComponent<SphereWorld>().SilentDestroy = true;
                    Destroy(_go);
                }
            } else {
                Debug.LogWarning($"Sphere {m_index} not exist.");
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
                    //random = new System.Random(100);
                }
                
                Vector3 pos = new Vector3(UnityEngine.Random.Range(0,100), 1.4f, UnityEngine.Random.Range(0, 100));
                Debug.Log($"x: {pos.x}, y: {pos.y}");
                return pos;
            }
        }

    }
}