using com.czeeep.network.player;
using com.czeeep.spell.biom;
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
    private GameObject _minePrefab;
    [SerializeField]
    private GameObject _blastPrefab;
    [SerializeField]
    private GameObject _selfMagicPrefab;
    [SerializeField]
    private bool _isMine;

    private Dictionary<string, ModificatorZone> _activeMagics = new Dictionary<string, ModificatorZone>();

    /*private Dictionary<string, string> _typeMagicByKey = new Dictionary<string, string>()
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
    };*/
    
    /*private Dictionary<int, string> _typeMagic = new Dictionary<int, string>()
    {
        {0b_0001,"spray" },
        {0b_0011,"lazer" },
        {0b_0111,"projectile" }
    };*/


    public void Init(List<MagicInfo> magics, bool ismine)
    {
        _magics = magics;
        _isMine = ismine;

        if (_magics.Count == 0)
        {
            StopShoot();
        }
        else
        {
            StartCoroutine(UpdateMagicsCoroutine());
        }
    } 

    public IEnumerator UpdateMagicsCoroutine()
    {
        yield return new WaitForEndOfFrame();
        UpdateMagics();
    }

    public void UpdateMagics()
    {
        string allMagicType = MagicType();
        CastDirection direction = CastDirection.forward;

        foreach (var magic in _magics)
        {
            if(magic.direction == CastDirection.self)
            {
                direction = CastDirection.self;
            }
        }


        
        foreach (var magic in _magics)
        {
            if (_activeMagics.TryGetValue(magic.key, out ModificatorZone zone))
            {
                zone.UpdateInfo(magic.key, magic.power);
                //_spheresCount[actSph] += 1;
            }
            else
            {
                switch (direction)
                {
                    case CastDirection.forward:
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
                                if (_isMine)
                                {
                                    Transform playerTransform = PlayerNetwork.LocalPlayerInstance.transform;
                                    Vector3 projPosition = playerTransform.position + playerTransform.forward * 1.1f;
                                    GameObject gop = PhotonNetwork.Instantiate(_projectilePrefab.name, projPosition, playerTransform.rotation);
                                    Projectile mzp = gop.GetComponent<Projectile>();
                                    mzp.UpdateInfo(magic.key, magic.power);
                                }
                                break;
                            case "mine":
                                if(_isMine) 
                                {
                                    /*Vector3 minepos = PlayerNetwork.LocalPlayerInstance.transform.position + PlayerNetwork.LocalPlayerInstance.transform.forward * 3f;
                                    PhotonNetwork.Instantiate(_minePrefab.name, minepos, Quaternion.identity); 
                                    */

                                    Transform mineTransform = PlayerNetwork.LocalPlayerInstance.transform;
                                    Vector3 minePosition = mineTransform.position + mineTransform.forward * 3f;
                                    GameObject gom = PhotonNetwork.Instantiate(_minePrefab.name, minePosition, mineTransform.rotation);
                                    Projectile mzm = gom.GetComponent<Projectile>();
                                    mzm.UpdateInfo(magic.key, magic.power);
                                }
                                break;
                            case "blast":
                                if (_isMine)
                                {
                                    Transform playerTransform = PlayerNetwork.LocalPlayerInstance.transform;
                                    Vector3 projPosition = playerTransform.position + playerTransform.forward * 1.1f;
                                    GameObject gop = PhotonNetwork.Instantiate(_blastPrefab.name, projPosition, playerTransform.rotation);
                                    Projectile mzp = gop.GetComponent<Projectile>();
                                    mzp.UpdateInfo(magic.key, magic.power);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case CastDirection.self:
                        GameObject goself= Instantiate(Resources.Load<GameObject>(_selfMagicPrefab.name), _content);
                        ModificatorZone mzself = goself.GetComponentInChildren<ModificatorZone>();
                        mzself.UpdateInfo(magic.key, magic.power);
                        _activeMagics.Add(magic.key, mzself);
                        break;
                    default:
                        break;

                }
            }
        }

        if (allMagicType.Equals("projectile"))
        {
            StartCoroutine(ShootStopCoroutine());
        }
    }


    public IEnumerator ShootStopCoroutine()
    {
        yield return new WaitForEndOfFrame();
        StaffModel.Instance.ShootStop();
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
        int result = 0b_00001;

        foreach (var magic in _magics)
        {
            switch (MagicConst.TYPE_MAGIC_BY_KEY[magic.key])
            {
                case "spray":
                    result = (result | 0b_00001);
                    break;
                case "lazer":
                    result = (result | 0b_00011);
                    break;
                case "projectile":
                    result = (result | 0b_00111);
                    break;
                case "mine":
                    result = (result | 0b_01111);
                    break;
                case "blast":
                    result = (result | 0b_11111);
                    break;
                default:
                    break;
            }
            Debug.Log($"MagicType {MagicConst.TYPE_MAGIC_BY_KEY[magic.key]} {result}");
        }


        return MagicConst.TYPE_MAGIC[result];
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
