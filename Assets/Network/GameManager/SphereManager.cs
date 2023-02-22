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
        const string TOTAL_SPHERE_COUNTS = "scount";
        const string SPHERE_PREFIX = "s";

        #endregion

        #region PrivateFields

        [SerializeField]
        GameObject spherePrefab;

        List<GameObject> _spheres = new List<GameObject>();

        [SerializeField]
        SphereConfig _config;

        #endregion

        #region Public Fields

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
                //Create hashtable
                ExitGames.Client.Photon.Hashtable htable = new ExitGames.Client.Photon.Hashtable();
                htable.Add(TOTAL_SPHERE_COUNTS, _config.MaxSpheresTotal);

                //Create spheres
                for (int i = 0; i < _config.MaxSpheresTotal; i++) {
                    //TODO Generate by group
                    string key = SPHERE_PREFIX + i.ToString();
                    SphereWorld sworld = CreateSphere(SphereConfig.GenerateRandomPosition(), Quaternion.identity);
                    sworld.SetIndex(key);
                    htable.Add(key, sworld.GetHashData());
                }
                //Set hash for replication
                PhotonNetwork.CurrentRoom.SetCustomProperties(htable);
            }
            
        }


        [PunRPC]
        protected SphereWorld CreateSphere(Vector3 pos, Quaternion rotation, int elementType = 0, int indx = -1) {
            GameObject _sphere = Instantiate(spherePrefab, null);
            _sphere.transform.position = pos;
            _sphere.transform.rotation = rotation;
            _spheres.Add(_sphere);
            var sworld = _sphere.GetComponent<SphereWorld>();
            sworld.Init((SpheresElements)elementType, 5, _spheres.Count-1);
            return sworld;
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
            static int base_spheres = (int)SpheresElements.life | (int)SpheresElements.fire | (int)SpheresElements.water | (int)SpheresElements.earth | (int)SpheresElements.freze | (int)SpheresElements.razor | (int)SpheresElements.dark | (int)SpheresElements.shield;
            static readonly int MAX_SPHERE_ID = (int)SpheresElements.shield; //0b_10000000000
            static readonly int BASE_SPHERE_COUNT = 8;
            [Tooltip("Зона распределения сфер")]
            public Rect rect;
            [Tooltip("Максимальное количество сфер")]
            public int MaxSpheresInGroup = 20;
            public int MaxSpheresTotal = 160;

            List<SpheresElements> baseSpheres;

            public SphereConfig() {
                //Prepare array of base elements
                CollectBaseSphereArray();
            }
            #region PRIVATE METHODS

            private void CollectBaseSphereArray() {
                baseSpheres = new List<SpheresElements>();
                foreach (SpheresElements element in (SpheresElements[])Enum.GetValues(typeof(SpheresElements))) {
                    if(IsBaseSphere((int)element)) {
                        baseSpheres.Add(element);
                    }
                }
            }
            #endregion
            public SpheresElements GetTypeShpere(int indx) {
                //Add Div

                return 0;
            }

            #region STATIC Methods

            /// <summary>
            /// Выдать новую случайную позицию.
            /// TODO
            /// </summary>
            /// <returns>Vector3, где по Y стоит статическая высота</returns>
            public static Vector3 GenerateRandomPosition() {
                
                Vector3 pos = new Vector3(UnityEngine.Random.Range(0,100), 1.4f, UnityEngine.Random.Range(0, 100));
                Debug.Log($"x: {pos.x}, z: {pos.z}");
                return pos;
            }

            /// <summary>
            /// Определить, является ли текущий тип сферы базовым.
            /// </summary>
            /// <param name="sphereType"></param>
            /// <returns></returns>
            public static bool IsBaseSphere(int sphereType) {
                return (base_spheres & sphereType) > 0;
            }

            #endregion
        }

        internal void CreateSpheres(ExitGames.Client.Photon.Hashtable hashtable) {
            // Replicate from serser
            int count = (int)hashtable[TOTAL_SPHERE_COUNTS];
            if(count > 0) {
                //Generate
                for (int i = 0; i < count; i++) {
                    byte[] itm = (byte[])hashtable[SPHERE_PREFIX + i.ToString()];
                    if (itm != null && itm.Length > 0) {
                        BitSphere bsphere = BitSphere.ConvertToSphere(itm);
                        //TODO Add INDEX
                        CreateSphere(bsphere.GetPosition(), Quaternion.identity, bsphere.GetIntSphereType());
                    }
                }
            }
        }
    }
}