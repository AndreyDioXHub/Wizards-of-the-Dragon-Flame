using com.czeeep.spell.staffmodel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInfo : MonoBehaviour
{
    public UnityEvent OnPlayerKnockout;
    public float MouseSensitivity { get => _mouseSensitivity; }
    public Vector3 ResultForceDirection { get => _resultForceDirection; }
    public float Speed { get => _speed * _slowdown; }
    public float Acceleration { get => _acceleration * _slowdown; }
    public bool IsStuned { get => _isStuned; }
    public float HitPointFill { get => (float)_hitPoint / (float)_hitPointMax; }
    public float ShieldPointFill { get => (float)_shieldPoint / (float)_shieldPointMax; }


    [SerializeField]
    private HitPointView _hpView;

    [SerializeField]
    private float _hitPointMax = 500;
    [SerializeField]
    private float _shieldPointMax = 500;
    [SerializeField]
    private float _mouseSensitivityMax = 1f;
    [SerializeField]
    private float _speedMax = 5;
    [SerializeField]
    private float _slowdownMax = 1;

    [SerializeField]
    private float _hitPoint = 500;
    [SerializeField]
    private float _shieldPoint = 0;
    [SerializeField]
    private float _mouseSensitivity = 1f;
    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private float _acceleration = 5;
    [SerializeField]
    private float _slowdown = 1;
    [SerializeField]
    private bool _isStuned = false;
    [SerializeField]
    private bool _isGhost = false;

    [SerializeField]
    private List<DirectionAndSpeed> _directions = new List<DirectionAndSpeed>();
    [SerializeField]
    private Vector3 _resultForceDirection;
    [SerializeField]
    private LayerMask _forceRayCastMask;

    private Dictionary<string, float> _slowdownDictionary = new Dictionary<string, float>()
    {
        {"base", 1 }
    };
    
    private Dictionary<string, bool> _isStunedDictionary = new Dictionary<string, bool>()
    {
        {"base", false }
    };
    private Dictionary<string, bool> _isGhostDictionary = new Dictionary<string, bool>()
    {
        {"base", false }
    };

    public void SetStun(string key, bool isStuned)
    {

        if (_isStunedDictionary.TryGetValue(key, out bool value))
        {
            _isStunedDictionary[key] = isStuned;
        }
        else
        {
            _isStunedDictionary.Add(key, isStuned);
        }

        bool isStunedTotal = false;

        foreach (var isd in _isStunedDictionary)
        {
            isStunedTotal = isStunedTotal || isd.Value;
        }

        _isStuned = isStunedTotal;

        if (_isStuned)
        {
            StaffModel.Instance.ShootStop();
        }
    }
    public void SetGhost(string key, bool isGhost)
    {

        if (_isGhostDictionary.TryGetValue(key, out bool value))
        {
            _isGhostDictionary[key] = isGhost;
        }
        else
        {
            _isGhostDictionary.Add(key, isGhost);
        }

        bool isGhostTotal = false;

        foreach (var issh in _isGhostDictionary)
        {
            isGhostTotal = isGhostTotal || issh.Value;
        }

        _isGhost = isGhostTotal;

        /*
        if (_isStuned)
        {
            StaffModel.Instance.ShootStop();
        }*/
    }

    public void AddForceVector(DirectionAndSpeed direction)
    {
        _directions.Add(direction);
    }

    public void SetHPView(HitPointView hpView)
    {
        _hpView = hpView;
    }

    public void UnArmor()
    {
        _shieldPoint = 0;
    }

    //must be private
    private void MakeShieldPointDamage(float damage, out float damageLeft)
    {
        _shieldPoint -= damage;
        damageLeft = 0;

        if (_shieldPoint < 0)
        {
            damageLeft = -_shieldPoint;
            _shieldPoint = 0;
        }

        if (_shieldPoint > _shieldPointMax)
        {
            _shieldPoint = _shieldPointMax;
        }

        if (_hpView != null)
        {
            _hpView.DrawHP(HitPointFill, ShieldPointFill);
        }

    }

    //must be private
    private void MakeHitPointDamage(float damage)
    {
        _hitPoint -= damage;

        if (_hitPoint > _hitPointMax)
        {
            _hitPoint = _hitPointMax;
        }

        if (_hitPoint <= 0)
        {
            OnPlayerKnockout?.Invoke();
        }

        if (_hpView != null)
        {
            _hpView.DrawHP(HitPointFill, ShieldPointFill);
        }
    }

    public void MakeDamage(float damage, float hpMultyplier = 1, float spMultyplier = 1)
    {
        if (_isGhost)
        {

        }
        else
        {
            if (spMultyplier != 0)
            {
                float damageLeft = 0;

                MakeShieldPointDamage(damage, out damageLeft);
                MakeHitPointDamage(hpMultyplier * (damageLeft / spMultyplier));
            }
            else
            {
                MakeHitPointDamage(hpMultyplier * damage);
            }
        }
    } 

    /*
    public void MakeDamage(float damage)
    {
        float damageLeft = 0;

        MakeShieldPointDamage(damage, out damageLeft);
        MakeHitPointDamage(damageLeft);
    }*/

    public void SpeedFraud(string key, float slowdown)
    {
        Debug.Log($"SpeedFraud income {key} {slowdown}");

        if (_slowdownDictionary.TryGetValue(key, out float value))
        {
            _slowdownDictionary[key] = slowdown;

            /*if(slowdown == 1)
            {
                _slowdownDictionary.Remove(key);
            }*/
        }
        else
        {
            _slowdownDictionary.Add(key, slowdown);
        }

        Debug.Log($"SpeedFraud Count {_slowdownDictionary.Count}");

        float slowdownTotal = _slowdownMax;

        foreach (var sd in _slowdownDictionary)
        {
            Debug.Log($"SpeedFraud {sd.Key} {sd.Value}");
            slowdownTotal = slowdownTotal * sd.Value;
        }

        _slowdown = slowdownTotal;

    }

    public void KnockoutRevive()
    {
        _hitPoint = _hitPointMax;
        _shieldPoint = _shieldPointMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _resultForceDirection = Vector3.zero;

        float weight = 0;
        float weightTotal = 0;

        foreach (var d in _directions)
        {
            weight++;
            weightTotal += weight;

            _resultForceDirection += d.direction * d.speed;// * weight;
        }

        foreach (var d in _directions)
        {
            d.speed -= d.vilosity * Time.deltaTime;
            d.speed = Mathf.Max(d.speed, 0);
        }

        Debug.DrawLine(transform.position, transform.position + _resultForceDirection.normalized, Color.blue);

        if (Physics.Raycast(transform.position, _resultForceDirection.normalized, out RaycastHit hit, 1, _forceRayCastMask))
        {
            Debug.Log($"swap {_directions.Count} ");

            foreach (var d in _directions)
            {
                Debug.Log($"swap {d.direction} ");
                d.direction = -1f * d.direction / 2f;
                Debug.Log($"swap {d.direction} ");
            }
        }

        List<DirectionAndSpeed> directionsForRemove = new List<DirectionAndSpeed>();


        for(int i=0; i< _directions.Count; i++)
        {
            if (_directions[i].speed == 0)
            {
                directionsForRemove.Add(_directions[i]);
            }
        }

        foreach(DirectionAndSpeed d in directionsForRemove)
        {
            _directions.Remove(d);
        }
    }
}

[Serializable]
public class DirectionAndSpeed
{
    public Vector3 direction;
    public float speed;
    public float vilosity;
    public DirectionAndSpeed(Vector3 direction, float speed, float vilosity)
    {
        this.direction = direction;
        this.speed = speed;
        this.vilosity = vilosity;
    }
}
