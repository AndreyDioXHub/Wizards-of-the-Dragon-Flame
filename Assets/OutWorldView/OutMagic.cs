using com.czeeep.spell.magic;
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
    private Dictionary<string, string> _typeMagic = new Dictionary<string, string>()
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

    public void Init(List<MagicInfo> magics)
    {
        _magics = magics;
        UpdateMagics();
    } 

    public void UpdateMagics()
    {
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
                switch (_typeMagic[magic.key])
                {
                    case "lazer":
                        GameObject gol = Instantiate(Resources.Load<GameObject>(_laserPrefab.name), _content);
                        ModificatorZone mzl = gol.GetComponent<ModificatorZone>();
                        mzl.UpdateInfo(magic.key, magic.power);
                        _activeMagics.Add(magic.key, mzl);
                        break;
                    case "spray":
                        GameObject gos = Instantiate(Resources.Load<GameObject>(_sprayPrefab.name), _content);
                        ModificatorZone mzs = gos.GetComponent<ModificatorZone>();
                        mzs.UpdateInfo(magic.key, magic.power);
                        _activeMagics.Add(magic.key, mzs);
                        break;
                    case "projectile":
                        GameObject gop = Instantiate(Resources.Load<GameObject>(_sprayPrefab.name), _content);
                        gop.transform.position = new Vector3(-5, -5, -5);
                        ModificatorZone mzp = gop.GetComponent<ModificatorZone>();
                        mzp.UpdateInfo(magic.key, magic.power);

                        break;
                    default:
                        break;
                }
                //GameObject go = Instantiate()
                //_spheresCount.Add(actSph, 1);
            }
        }
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
