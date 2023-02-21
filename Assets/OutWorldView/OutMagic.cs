using com.czeeep.network.player;
using com.czeeep.spell.magic;
using com.czeeep.spell.staffmodel;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutMagic : MonoBehaviour
{
    [SerializeField]
    private List<MagicInfo> _magics = new List<MagicInfo>();
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _sprayPrefab;
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private GameObject _selfMagicPrefab;

    private Dictionary<string, ModificatorZone> _activeMagics = new Dictionary<string, ModificatorZone>();

    private Dictionary<string, string> _typeMagicByKey = new Dictionary<string, string>()
    {
        {"life","lazer" },
        {"fire","spray" },
        {"water","spray" },
        {"earth","projectile" },
        {"freze","spray" },
        {"razor","lazer" },
        {"dark","lazer" },
        {"steam","spray" },
        {"poison","spray" },
        {"ice","projectile" },
        {"shield","lazer" },
    };
    
    private Dictionary<int, string> _typeMagic = new Dictionary<int, string>()
    {
        {0b_001,"spray" },
        {0b_011,"lazer" },
        {0b_111,"projectile" }
    };


    public void Init(List<MagicInfo> magics, bool ismine)
    {
        _magics = magics;
        if(_magics.Count == 0)
        {
            StopShoot();
        }
        else
        {
            UpdateMagics();
        }
    } 

    public void UpdateMagics()
    {
        string allMagicType = MagicType();

        foreach (var magic in _magics)
        {
            if (_activeMagics.TryGetValue(magic.key, out ModificatorZone zone))
            {
                zone.UpdateInfo(magic.key, magic.power);
                //_spheresCount[actSph] += 1;
            }
            else
            {
                //string type = "";
                switch (allMagicType)
                {
                    case "lazer":
                        GameObject gol = Instantiate(Resources.Load<GameObject>(_laserPrefab.name), _content);
                        ModificatorZone mzl = gol.GetComponentInChildren<ModificatorZone>();
                        mzl.UpdateInfo(magic.key, magic.power);
                        _activeMagics.Add(magic.key, mzl);
                        break;
                    case "spray":
                        GameObject gos = Instantiate(Resources.Load<GameObject>(_sprayPrefab.name), _content);
                        ModificatorZone mzs = gos.GetComponentInChildren<ModificatorZone>();
                        mzs.UpdateInfo(magic.key, magic.power);
                        _activeMagics.Add(magic.key, mzs);
                        break;
                    case "projectile":
                        Transform playerTransform = PlayerNetwork.LocalPlayerInstance.transform;
                        Vector3 projPosition = playerTransform.position + playerTransform.forward * 1.2f;
                        //GameObject gop = PhotonNetwork.Instantiate(Resources.Load<GameObject>(_projectilePrefab.name), _content);
                        GameObject gop = PhotonNetwork.Instantiate(_projectilePrefab.name, projPosition, playerTransform.rotation);
                        //gop.transform.localPosition = new Vector3(0, 0, 1.2f);
                        Projectile mzp = gop.GetComponent<Projectile>();
                        mzp.UpdateInfo(magic.key, magic.power);
                        //gop.transform.SetParent(null);
                        //if()
                        //Photon.
                        break;
                    default:
                        break;
                }

                //GameObject go = Instantiate()
                //_spheresCount.Add(actSph, 1);
            }
        }

        if (allMagicType.Equals("projectile"))
        {
            StaffModel.Instance.ShootStop();
        }
    }

    public void StopShoot()
    {
        foreach(var am in _activeMagics)
        {
            am.Value.DestroyZone();
        }

        _activeMagics.Clear();
        _activeMagics = new Dictionary<string, ModificatorZone>();
    }

    public string MagicType()
    {
        //string result = "";
        int result = 0b_001;

        foreach (var magic in _magics)
        {
            switch (_typeMagicByKey[magic.key])
            {
                case "spray":
                    result = (result | 0b_001);
                    break;
                case "lazer":
                    result = (result | 0b_011);
                    break;
                case "projectile":
                    result = (result | 0b_111);
                    break;
                default:
                    break;
            }
            Debug.Log($"MagicType {_typeMagicByKey[magic.key]} {result}");
        }


        return _typeMagic[result];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
