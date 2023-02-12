using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Staff : MonoBehaviour
{
    //public UnityEvent OnShoot;
    public bool IsShoot  { get => _isShoot; }
    
    [SerializeField]
    private Tick _tick;
    [SerializeField]
    private CastModel _castModel;

    [SerializeField]
    private CastDirection _direction;


    [SerializeField]
    private List<GameObject> _magics = new List<GameObject>();

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (_isShoot)
        {   
            string spheres = "";

            foreach(var sp in _castModel.ActiveSpheres)
            {
                spheres += $" {sp}";
            }

            Debug.Log($"is shoot {spheres}");
        }*/
    }

    public void Shoot(CastDirection direction)
    {
        if (_mayShoot)
        {
            _tick.UpdateTick();

            if (!_magicInited)
            {
                foreach (var sp in _castModel.ActiveSpheres)
                {
                   
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
    }

    public void UpdateMayShoot()
    {
        _mayShoot = true;

        if (_isShoot)
        {
            _castModel.ReloadActiveSpheres();
        }
    }
}
