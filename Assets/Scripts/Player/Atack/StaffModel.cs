using com.czeeep.network.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaffModel : MonoBehaviour
{
    public UnityEvent<Dictionary<string, int>, CastDirection> OnStaffShoot;
    public UnityEvent<bool> OnStaffShootStop;

    public static StaffModel Instance;
    //public UnityEvent OnShoot;
    public bool IsShoot  { get => _isShoot; }

    [SerializeField]
    private PlayerNetwork _player;

    [SerializeField]
    private Tick _tick;

    [SerializeField]
    private CastDirection _direction;

    private Dictionary<string, Magic> _magics = new Dictionary<string, Magic>();
    private Dictionary<string, int> _spheresCount = new Dictionary<string, int>();
    [SerializeField]
    private bool _mayShoot;
    [SerializeField]
    private bool _isShoot;
    [SerializeField]
    private bool _magicInited;

    private Dictionary<string, string> _magicsList = new Dictionary<string, string>() 
    {
        {"life","Magic" },
        {"fire","Magic" },
        {"water","Magic" },
        {"earth","Magic" },
        {"freze","Magic" },
        {"razor","Magic" },
        {"dark","Magic" },
        {"steam","Magic" },
        {"poison","Magic" },
        {"ice","Magic" },
        {"shield","Magic" },
    };


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Init(PlayerNetwork player)
    {
        _player = player;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(CastDirection direction)
    {
        if (_mayShoot)
        {
            Debug.Log("StaffModel Shoot");
            _tick.UpdateTick();
            
            if (_magicInited)
            {
                

            }
            else
            {
                UpdateMagic();

                if(_spheresCount.Count > 0)
                {
                    foreach (var sphC in _spheresCount)
                    {
                        GameObject go = Instantiate(Resources.Load<GameObject>(_magicsList[sphC.Key]), _player.transform);

                        Magic magic = go.GetComponent<Magic>();
                        magic.UpdateInfo(new MagicInfo(sphC.Key, _direction, sphC.Value));

                        _magics.Add(sphC.Key, magic);
                    }

                    _magicInited = true;
                }
                else
                {
                    Debug.Log("chunk to the head");
                }

            }

            _direction = direction;
            _isShoot = true;
            _mayShoot = false;
        }
    }

    public void ShootStop()
    {
        _isShoot = false;
        //MagicModel.Instance.ReloadActiveSpheres();

        if (_magicInited)
        {

            foreach (var magic in _magics)
            {
                magic.Value.DestroyMagic();
            }

            _magics = null;
            _magics = new Dictionary<string, Magic>();

            _magicInited = false;
        }
    }

    public void UpdateMayShoot()
    {
        _mayShoot = true;

        if (_isShoot)
        {
            MagicModel.Instance.ReloadActiveSpheres();
        }

        if (_magicInited)
        {
            UpdateMagic();
        }
        

    }

    public void UpdateMagic()
    {
        _spheresCount.Clear();

        _spheresCount = new Dictionary<string, int>();

        foreach (var actSph in MagicModel.Instance.ActiveSpheres)
        {
            if (_spheresCount.TryGetValue(actSph, out int value))
            {
                _spheresCount[actSph] += 1;
            }
            else
            {
                _spheresCount.Add(actSph, 1);
            }
        }

        if (_magics.Count > 0)
        {
            foreach (var sphC in _spheresCount)
            {
                if (_magics.TryGetValue(sphC.Key, out Magic magic))
                {
                    _magics[sphC.Key].UpdateInfo(new MagicInfo(sphC.Key, _direction, sphC.Value));
                }
            }

            CleareCrossSpheres();

        }

    }

    private void CleareCrossSpheres()
    {
        List<string> incommingMagics = new List<string>();

        foreach (var sc in _spheresCount)
        {
            incommingMagics.Add(sc.Key);
        }

        List<string> curentMagics = new List<string>();

        foreach (var sc in _magics)
        {
            curentMagics.Add(sc.Key);
        }

        List<string> crossMagic = new List<string>();

        foreach (var cm in curentMagics)
        {
            string f = incommingMagics.Find(im => im == cm);
            if (string.IsNullOrEmpty(f))
            {
                crossMagic.Add(cm);
            }

        }

        foreach (var cross in crossMagic)
        {
            _magics[cross].DestroyMagic();
            _magics.Remove(cross);
        }

        if (_magics.Count == 0)
        {
            _magicInited = false;
        }
    }
}
