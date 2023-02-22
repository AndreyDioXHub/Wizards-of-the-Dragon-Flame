using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInfo : MonoBehaviour
{
    public UnityEvent OnPlayerKnockout;
    public float MouseSensitivity { get => _mouseSensitivity; }
    public float Speed { get => _speed; }


    [SerializeField]
    private int _hitPointMax = 100;
    [SerializeField]
    private float _mouseSensitivityMax = 100f;
    [SerializeField]
    private float _speedMax = 5;

    [SerializeField]
    private int _hitPoint = 100;
    [SerializeField]
    private float _mouseSensitivity = 100f;
    [SerializeField]
    private float _speed = 5;

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

    //public void 

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
