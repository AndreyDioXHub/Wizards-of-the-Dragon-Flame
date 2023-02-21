using com.czeeep.spell.magic;
using com.czeeep.spell.staffmodel;
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
        {"shield","laser" },
    };
    
    private Dictionary<int, string> _typeMagic = new Dictionary<int, string>()
    {
        {0b_001,"spray" },
        {0b_011,"lazer" },
        {0b_111,"projectile" }
    };


    public void Init(List<MagicInfo> magics)
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
                        GameObject gop = Instantiate(Resources.Load<GameObject>(_projectilePrefab.name), _content);
                        gop.transform.position = new Vector3(-5, -5, -5);
                        ModificatorZone mzp = gop.GetComponentInChildren<ModificatorZone>();
                        mzp.UpdateInfo(magic.key, magic.power);
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
