using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInfo : MonoBehaviour
{
    public UnityEvent OnPlayerKnockout;
    public float MouseSensitivity { get => _mouseSensitivity; }
    public float Speed { get => _speed * _slowdown; }
    public float HitPointFill { get => (float)_hitPoint / (float)_hitPointMax; }
    public float ShieldPointFill { get => (float)_shieldPoint / (float)_shieldPointMax; }


    [SerializeField]
    private HitPointView _hpView;

    [SerializeField]
    private int _hitPointMax = 100;
    [SerializeField]
    private int _shieldPointMax = 100;
    [SerializeField]
    private float _mouseSensitivityMax = 100f;
    [SerializeField]
    private float _speedMax = 5;
    [SerializeField]
    private float _slowdownMax = 1;

    [SerializeField]
    private int _hitPoint = 100;
    [SerializeField]
    private int _shieldPoint = 100;
    [SerializeField]
    private float _mouseSensitivity = 100f;
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _slowdown = 1;

    private Dictionary<string, float> _slowdownDictionary = new Dictionary<string, float>()
    {
        {"base", 1 }
    };

    public void SetHPView(HitPointView hpView)
    {
        _hpView = hpView;
    }

    public void UnArmor()
    {
        _shieldPoint = 0;
    }

    public void MakeShieldPointDamage(int damage, out int damageLeft)
    {
        _shieldPoint -= damage;
        damageLeft = 0;

        if (_shieldPoint < 0)
        {
            damageLeft = -_shieldPoint;
            _shieldPoint = 0;
        }

        if (_hpView != null)
        {
            _hpView.DrawHP(HitPointFill, ShieldPointFill);
        }

    }

    public void MakeHitPointDamage(int damage)
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

    public void MakeDamage(int damage)
    {
        int damageLeft = 0;

        MakeShieldPointDamage(damage, out damageLeft);
        MakeHitPointDamage(damageLeft);
    }

    public void SpeedFraud(string key, float slowdown)
    {
        Debug.Log($"SpeedFraud income {key} {slowdown}");

        if (_slowdownDictionary.TryGetValue(key, out float value))
        {
            _slowdownDictionary[key] = slowdown;

            if(slowdown == 1)
            {
                _slowdownDictionary.Remove(key);
            }
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
        
    }
}
