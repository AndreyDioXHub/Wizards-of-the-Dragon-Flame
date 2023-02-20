using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float MouseSensitivity { get => _mouseSensitivity; }
    public float Speed { get => _speed; }

    [SerializeField]
    private float _mouseSensitivity = 100f;
    [SerializeField]
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
