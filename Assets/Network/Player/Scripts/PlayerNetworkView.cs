using com.czeeep.spell.magic;
using com.czeeep.spell.modificator;
using com.czeeep.spell.staffmodel;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerNetworkView : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private List<GameObject> _exceptionGameObjects = new List<GameObject>();
    [SerializeField]
    private string _mySpheresMagicModificators;
    [SerializeField]
    private string _mySpheresMagicModificatorsPrev;

    [SerializeField]
    private List<MagicInfo> _magics = new List<MagicInfo>();
    [SerializeField]
    private List<ModificatorInfo> _modificators = new List<ModificatorInfo>();
    [SerializeField]
    private List<string> _spheres = new List<string>();

    [SerializeField]
    private List<bool> _changes = new List<bool>();

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
            CheckSplitMagicString(out _changes);
            _mySpheresMagicModificatorsPrev = _mySpheresMagicModificators;
        }

        //_random = Random.Range(-1000, 1000);

    }


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
                string clearPart = p.Remove(0,1);
                string[] clearPartSplited = clearPart.Split(':');
                switch (first)
                {
                    case 's':

                        MagicInfo magInfo = new MagicInfo(clearPartSplited[0], (CastDirection)int.Parse(clearPartSplited[1]), int.Parse(clearPartSplited[2]));
                        /*modInfo.key = clearPartSplited[0];
                        modInfo.power = int.Parse(clearPartSplited[1]);
                        modInfo.time = float.Parse(clearPartSplited[2], CultureInfo.InvariantCulture.NumberFormat);*/
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

        if(_magics.Count != magics.Count)
        {
            magicChanged = true;
        }
        else
        {
            for (int i = 0; i < _magics.Count - 1; i++)
            {
                if(!_magics[i].name.Equals(magics[i].name) || _magics[i].power != magics[i].power)
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
                if (!_spheres[i].Equals(spheres[i]) )
                {
                    sphChanged = true;
                }
            }
        }
        _spheres = spheres;

        changes[0] = magicChanged;
        changes[1] = modChanged;
        changes[2] = sphChanged;
    }

    public string CreateSpheresMagicModificatorsString()
    {
        string result = "";
        List<GameObject> childs = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            bool isAdd = true;
            GameObject go = transform.GetChild(i).gameObject;
            foreach (var ex in _exceptionGameObjects)
            {
                if(go == ex)
                {
                    isAdd = false;
                }
            }
            if (isAdd)
            {
                childs.Add(go);
            }
        }

        foreach (GameObject child in childs)
        {
            result += $"{child.name}|";
            //child.transform.SetParent(null);
            //Destroy(child);
        }

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
