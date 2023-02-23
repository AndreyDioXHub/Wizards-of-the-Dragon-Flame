using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInfo : MonoBehaviour
{
    public UnityEvent OnPlayerKnockout;
    public float MouseSensitivity { get => _mouseSensitivity; }
    public float Speed { get => _speed * _slowdown; }


    [SerializeField]
    private int _hitPointMax = 100;
    [SerializeField]
    private float _mouseSensitivityMax = 100f;
    [SerializeField]
    private float _speedMax = 5;
    [SerializeField]
    private float _slowdownMax = 1;

    [SerializeField]
    private int _hitPoint = 100;
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

    public void MakeDamage(int damage)
    {
        _hitPoint -= damage;

        if(_hitPoint > _hitPointMax)
        {
            _hitPoint = _hitPointMax;
        }

        if(_hitPoint <= 0)
        {
            OnPlayerKnockout?.Invoke();
        }

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
