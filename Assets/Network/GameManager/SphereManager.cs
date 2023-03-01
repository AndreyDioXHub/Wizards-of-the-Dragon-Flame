using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using com.czeeep.spell.magicmodel;

namespace com.czeeep.network {

    public class SphereManager : MonoBehaviour {
        #region Constants

        public const string CREATE_SPHERE_ACTION = "CreateSphere";
        public const string DESTROY_SPHERE_ACTION = "RemovedExtenral";
        public const string REMOVE_SPHERE_FROM_WORLD = "RemoveSphereFromWorld";
        const string TOTAL_SPHERE_COUNTS = "scount";
        const string SPHERE_PREFIX = "s";
        const string PLAYER_PREFIX = "p";

        #endregion

        #region PrivateFields

        [SerializeField]
        GameObject spherePrefab;

        Dictionary<int, GameObject> _spheresDict = new Dictionary<int, GameObject>();

        [SerializeField]
        SphereConfig _config;

        Transform spheresRoot { get { 
                if (_spheresRoot == null) {
                    GameObject go = new GameObject("spheres pool");
                    _spheresRoot = go.transform;
                }
                return _spheresRoot;
                } }
        Transform _spheresRoot = null;

        #endregion

        #region Public Fields

        public PhotonView photonView { get; set; }


        #endregion

        #region MonoBehaviour callback
        private void Awake() {
            if (_config == null) {
                Debug.Log("Create config: <b>Later load it from settings</b>");
                _config = new SphereConfig();
            } else {
                _config.Init();
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
                    SphereWorld sworld = CreateSphere(SphereConfig.GenerateRandomPosition(), Quaternion.identity,_config.GetTypeShpere(i), i);
                    sworld.SetIndex(key);
                    htable.Add(key, sworld.GetHashData());
                }
                //Set hash for replication
                PhotonNetwork.CurrentRoom.SetCustomProperties(htable);
            }
            
        }


        [PunRPC]
        protected SphereWorld CreateSphere(Vector3 pos, Quaternion rotation, int elementType, int indx) {
            GameObject _sphere = Instantiate(spherePrefab, spheresRoot);
            _sphere.transform.position = pos;
            _sphere.transform.rotation = rotation;
            //_spheres.Add(_sphere);
            if(_spheresDict.ContainsKey(indx)) {
                Debug.LogWarning($"SphereManager.CreateSphere: Key {indx} already exist in sphere dictionary!<b> It will not be updated!</b>");
            } else {
                _spheresDict.Add(indx, _sphere);
            }
            var sworld = _sphere.GetComponent<SphereWorld>();
            sworld.Init((SpheresElements)elementType, _config.DefaultAmount, indx);
            sworld.GetBiomUnderSphere();
            return sworld;
        }

        internal void WillDestroyed(int m_index) {
            //Debug.Log($"<color=red>SphereManager: <b>WillDestroyed</b> called. Sphere index: {m_index}</color>");

            int caller = PhotonNetwork.LocalPlayer.ActorNumber;
            if(PhotonNetwork.IsConnected) {
                photonView.RPC(DESTROY_SPHERE_ACTION, RpcTarget.MasterClient, m_index, caller);
            } else {
                Debug.Log($"<color=red>Not connected. Work locally</color>");
                RemovedExtenral(m_index, caller);
            }
        }

        [PunRPC]
        public void RemovedExtenral(int m_index, int caller) {
            //Debug.Log($"<color=red>SphereManager: <b>RemovedExtenral</b> called. Sphere index: {m_index}, Caller id: {caller}</color>");

            #region Only MASTER client actions
            //Debug.Log($"<color=red>SphereManager: <b>WillDestroyed</b> called. Sphere index: {m_index}</color>");
            if (PhotonNetwork.IsMasterClient) {
                //Exist in hash?
                string hashkey = SPHERE_PREFIX + m_index.ToString();
                var hashtable = PhotonNetwork.CurrentRoom.CustomProperties;
                if (hashtable.ContainsKey(hashkey)) {
                    //Записать владельцу добавление данных из сферы
                    byte[] _bsbyte = (byte[])hashtable[hashkey];
                    BitSphere _bs = BitSphere.ConvertToSphere(_bsbyte);
                    _bs.sphereID = (ushort)caller;
                    //Удалить сферу из общего списка
                    hashtable[hashkey] = null;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
                    //Destroy GO - отдельный RPC для всех?
                    photonView.RPC(REMOVE_SPHERE_FROM_WORLD, RpcTarget.All,  m_index, _bs.GetBytes8());
                }
            }

            #endregion
        }

        /// <summary>
        /// Удаление GO 
        /// </summary>
        /// <param name="m_index"></param>
        [PunRPC]
        public void RemoveSphereFromWorld(int m_index, byte[] updated) {
            //Debug.Log($"<color=red>SphereManager: <b>RemoveSphereFromWorld</b> called. Sphere index: {m_index}</color>");
            if(_spheresDict.ContainsKey(m_index)) {
                GameObject sgo;
                if(_spheresDict.TryGetValue(m_index, out sgo)) {
                    _spheresDict.Remove(m_index);
                    Destroy(sgo);

                }
            } else {
                Debug.LogWarning($"Sphere {m_index} <b>not found in dictionary.</b>");
            }
            //Add removed to his owner player
            BitSphere _bs = BitSphere.ConvertToSphere(updated);
            if(PhotonNetwork.LocalPlayer.ActorNumber == (int)_bs.sphereID) {
                Debug.Log($"SphereManager: <b>ADD {_bs.amount} {(SpheresElements)_bs.GetIntSphereType()} FOR ME: </b> {m_index}");
                if(_bs.sphereType == 0) {
                    Debug.Log($"SphereManager: <b>Try add empty sphere to user {_bs.sphereID}</b>. Sphere index: {m_index}, type: {_bs.sphereType}, amount: {_bs.amount}");
                }
                MagicModel.Instance.AddSphere(((SpheresElements)_bs.GetIntSphereType()).ToString(), _bs.amount);
            }
        }

        #endregion

        [Serializable]
        public class SphereConfig {
            static int base_spheres = (int)SpheresElements.life | (int)SpheresElements.fire | (int)SpheresElements.water | (int)SpheresElements.earth | (int)SpheresElements.freeze | (int)SpheresElements.razor | (int)SpheresElements.dark | (int)SpheresElements.shield;
            [Tooltip("Зона распределения сфер")]
            public Rect rect;
            [Tooltip("Количество сфер в группе")]
            int MaxSpheresInGroup = 0;
            [Tooltip("Общее начальное количество сфер на игру")]
            public int MaxSpheresTotal = 160;

            public static SphereConfig Instance;

            List<SpheresElements> baseSpheres;

            public int DefaultAmount { get; internal set; } = 5;

            public SphereConfig() {
                Instance = this;
                Init();
            }

            public void Init() {
                //Prepare array of base elements
                CollectBaseSphereArray();
                MaxSpheresInGroup = MaxSpheresTotal / baseSpheres.Count;
                Debug.Log($"MaxSpheresInGroup = {MaxSpheresInGroup}");
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
            public int GetTypeShpere(int indx) {
                //Add Div
                int part = indx / MaxSpheresInGroup;
                if(part < baseSpheres.Count) {
                    return (int)baseSpheres[part];
                } else {
                    return 0;
                }
            }

            #region STATIC Methods

            /// <summary>
            /// Выдать новую случайную позицию.
            /// TODO
            /// </summary>
            /// <returns>Vector3, где по Y стоит статическая высота</returns>
            public static Vector3 GenerateRandomPosition() {
                int xmax = Instance == null ? 100 : (int)Instance.rect.width;
                int ymax = Instance == null ? 100 : (int)Instance.rect.height;
                Vector3 pos = new Vector3(UnityEngine.Random.Range(0,xmax), 1.4f, UnityEngine.Random.Range(0, ymax));
                //Debug.Log($"x: {pos.x}, z: {pos.z}");
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
            Debug.Log($"Count of elements: {count}");
            if(count > 0) {
                //Generate
                for (int i = 0; i < count; i++) {
                    byte[] itm = (byte[])hashtable[SPHERE_PREFIX + i.ToString()];
                    if (itm != null && itm.Length > 0) {
                        BitSphere bsphere = BitSphere.ConvertToSphere(itm);
                        //TODO Add INDEX
                        SphereWorld bs = CreateSphere(bsphere.GetPosition(), Quaternion.identity, bsphere.GetIntSphereType(),i);
                        bs.SetIndex(SPHERE_PREFIX + i.ToString());
                        //Update position for sphere
                        bs.UpdatePositionByBitSphere();
                    }
                }
            }
        }
    }
}