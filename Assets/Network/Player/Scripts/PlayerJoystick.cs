using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystick : MonoBehaviour
{
    [SerializeField]
    private Transform _pointWorld;
    [SerializeField]
    private float _angle = 45;
    [SerializeField]
    private Vector2 _mousePos;
    [SerializeField]
    private Vector3 _mousePosWorld;
    [SerializeField]
    private Vector2 _screen;


    // Start is called before the first frame update
    void Start()
    {
        _screen = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        _mousePos = Input.mousePosition;
        _mousePos.x = (_mousePos.x - _screen.x/2)/10;
        _mousePos.y = (_mousePos.y - _screen.y/2)/10;
        _mousePosWorld = new Vector3(_mousePos.x, 0, _mousePos.y);
        _mousePosWorld = Quaternion.Euler(0, _angle, 0) * _mousePosWorld;
        _pointWorld.position = transform.position + _mousePosWorld;//new Vector3(_mousePos.x + transform.position.x, 0, _mousePos.y + transform.position.z);

        //_pointWorld.position = Quaternion.Euler(0, _angle, 0) * _pointWorld.position;
    }
}