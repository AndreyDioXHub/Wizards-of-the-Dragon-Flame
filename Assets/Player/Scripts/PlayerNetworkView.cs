using com.czeeep.spell.magic;
using com.czeeep.spell.modificator;
using com.czeeep.spell.staffmodel;
using Newtonsoft.Json;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

namespace com.czeeep.network.player
{
    public class PlayerNetworkView : MonoBehaviourPunCallbacks, IPunObservable
    {
        //public UnityEvent<TransferMagicItems> OnTransferChanged;
        public UnityEvent<List<MagicInfo>, bool> OnMagicChanged;
        //public UnityEvent OnMagicEmpty;

        public UnityEvent<List<ModificatorInfo>> OnModificatorChanged;
        public UnityEvent<List<string>> OnSphereChanged;

        [SerializeField]
        private PlayerNetwork _player;
        [SerializeField]
        private List<GameObject> _exceptionGameObjects = new List<GameObject>();
        [SerializeField]
        private string _mySpheresMagicModificators;
        [SerializeField]
        private string _mySpheresMagicModificatorsPrev;
        [SerializeField]
        private TransferMagicItems _transferMagicItems;

        /*[SerializeField]
        private List<MagicInfo> _magics = new List<MagicInfo>();
        [SerializeField]
        private List<ModificatorInfo> _modificators = new List<ModificatorInfo>();
        [SerializeField]
        private List<string> _spheres = new List<string>();*/

        /*[SerializeField]
        private List<bool> _changes = new List<bool>();*/

        // Start is called before the first frame update
        void Start()
        {
            if (photonView.IsMine)
            {
                /*StaffModel.Instance.OnStaffShoot.AddListener(OnStaffShoot);
                StaffModel.Instance.OnStaffShootStop.AddListener(OnStaffShootStop);*/
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (photonView.IsMine)
            {
                _mySpheresMagicModificators = CreateSpheresMagicModificatorsString();
                //Destroy(_character);
            }
            /*else
            {
                return;
            }*/
            
            if (!_mySpheresMagicModificators.Equals(_mySpheresMagicModificatorsPrev))
            {
                _transferMagicItems = DeserializeMagicString();

                OnMagicChanged?.Invoke(_transferMagicItems.magics, photonView.IsMine);
                OnModificatorChanged?.Invoke(_transferMagicItems.modificators);
                OnSphereChanged?.Invoke(_transferMagicItems.spheres);

                _mySpheresMagicModificatorsPrev = _mySpheresMagicModificators;
            }

            //_random = Random.Range(-1000, 1000);

        }

        public TransferMagicItems DeserializeMagicString()
        {
            Debug.Log($"Deserialized");

            return JsonConvert.DeserializeObject<TransferMagicItems>(_mySpheresMagicModificators);
        }

        /*
        public void CheckSplitMagicString(out List<bool> changes)
        {
            Debug.Log($"splited");

            //spell: selement:intdirection:power
            //modificator: melement:power:time
            //element: eelement

            //magic, modificator, sphere
            changes = new List<bool> { false, false, false };

            //bool someChanged = 

            string[] parts = _mySpheresMagicModificators.Split('|');

            List<MagicInfo> magics = new List<MagicInfo>();
            List<ModificatorInfo> modificators = new List<ModificatorInfo>();
            List<string> spheres = new List<string>();

            foreach (var p in parts)
            {
                // Debug.Log(p);
                if (p.Length > 0)
                {
                    char first = p[0];
                    string clearPart = p.Remove(0, 1);
                    string[] clearPartSplited = clearPart.Split(':');
                    switch (first)
                    {
                        case 's':

                            MagicInfo magInfo = new MagicInfo(clearPartSplited[0], (CastDirection)int.Parse(clearPartSplited[1]), int.Parse(clearPartSplited[2]));
                            
                            magics.Add(magInfo);

                            break;
                        case 'm':
                            ModificatorInfo modInfo = new ModificatorInfo();
                            modInfo.key = clearPartSplited[0];
                            modInfo.power = int.Parse(clearPartSplited[1]);
                            modInfo.time = float.Parse(clearPartSplited[2], CultureInfo.InvariantCulture.NumberFormat);
                            modificators.Add(modInfo);
                            break;
                        case 'e':
                            spheres.Add(clearPart);
                            break;
                        default:
                            break;
                    }
                }
            }

            bool magicChanged = false;

            if (_magics.Count != magics.Count)
            {
                magicChanged = true;
            }
            else
            {
                for (int i = 0; i < _magics.Count - 1; i++)
                {
                    if (!_magics[i].key.Equals(magics[i].key) || _magics[i].power != magics[i].power)
                    {
                        magicChanged = true;
                    }
                }
            }

            _magics = magics;

            bool modChanged = false;

            if (_modificators.Count != modificators.Count)
            {
                modChanged = true;
            }
            else
            {
                for (int i = 0; i < _modificators.Count - 1; i++)
                {
                    if (!_modificators[i].key.Equals(modificators[i].key) || _modificators[i].power != modificators[i].power)
                    {
                        modChanged = true;
                    }
                }
            }
            _modificators = modificators;

            bool sphChanged = false;

            if (_spheres.Count != spheres.Count)
            {
                sphChanged = true;
            }
            else
            {
                for (int i = 0; i < _spheres.Count - 1; i++)
                {
                    if (!_spheres[i].Equals(spheres[i]))
                    {
                        sphChanged = true;
                    }
                }
            }
            _spheres = spheres;

            changes[0] = magicChanged;
            changes[1] = modChanged;
            changes[2] = sphChanged;
        }*/

        public string CreateSpheresMagicModificatorsString()
        {
            string result = "";
            List<GameObject> childsMagic = new List<GameObject>();
            List<GameObject> childsModificator = new List<GameObject>();
            List<GameObject> childsSphere = new List<GameObject>();

            for (int i = 0; i < _player.ModelBackPackMagic.childCount; i++)
            {
                bool isAdd = true;
                GameObject go = _player.ModelBackPackMagic.GetChild(i).gameObject;
                foreach (var ex in _exceptionGameObjects)
                {
                    if (go == ex)
                    {
                        isAdd = false;
                    }
                }
                if (isAdd)
                {
                    childsMagic.Add(go);
                }
            }

            for (int i = 0; i < _player.ModelBackPackModificator.childCount; i++)
            {
                bool isAdd = true;
                GameObject go = _player.ModelBackPackModificator.GetChild(i).gameObject;
                foreach (var ex in _exceptionGameObjects)
                {
                    if (go == ex)
                    {
                        isAdd = false;
                    }
                }
                if (isAdd)
                {
                    childsModificator.Add(go);
                }
            }

            for (int i = 0; i < _player.ModelBackPackSphere.childCount; i++)
            {
                bool isAdd = true;
                GameObject go = _player.ModelBackPackSphere.GetChild(i).gameObject;
                foreach (var ex in _exceptionGameObjects)
                {
                    if (go == ex)
                    {
                        isAdd = false;
                    }
                }
                if (isAdd)
                {
                    childsSphere.Add(go);
                }
            }

            string resultMagic = "";

            foreach (GameObject child in childsMagic)
            {
                resultMagic += $"{child.name},";
            }
            //resultMagic.Remove(resultMagic.Length - 2, 1);

            string resultMod= "";

            foreach (GameObject child in childsModificator)
            {
                resultMod += $"{child.name},";
            }
            //resultMod.Remove(resultMod.Length - 2, 1);

            string resultSp= "";

            foreach (GameObject child in childsSphere)
            {
                resultSp += $"\"{child.name}\",";
            }
            //resultSp.Remove(resultSp.Length-2, 1);

            result = $"{"{"}\"magics\":[{resultMagic}],\"modificators\":[{resultMod}],\"spheres\":[{resultSp}]{"}"}";

            /*foreach (GameObject child in childs)
            {
                result += $"{child.name}|";
                //child.transform.SetParent(null);
                //Destroy(child);
            }*/

            return result;
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                //We own this player: send the others out data
                //stream.SendNext(_random);
                stream.SendNext(_mySpheresMagicModificators);
            }
            else
            {
                //this._random = (int)stream.ReceiveNext();
                this._mySpheresMagicModificators = (string)stream.ReceiveNext();
            }
        }

        private void OnDestroy()
        {
        }
    }

    [Serializable]
    public class TransferMagicItems
    {
        public List<MagicInfo> magics = new List<MagicInfo>();
        public List<ModificatorInfo> modificators = new List<ModificatorInfo>();
        public List<string> spheres = new List<string>();
    }
}
