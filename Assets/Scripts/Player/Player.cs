using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private CharacterController _character;
    [SerializeField]
    private CastModel _castModel;

    [SerializeField]
    private Transform _point;

    [SerializeField]
    private Camera _playerCamera;
    [SerializeField]
    private float _mouseSensitivity = 100f;
    [SerializeField]
    private float _speed;

    private float _xRotation = 0f;
    private float _angleTrashHold = 1f;
    private float _positionTrashHold = 0.5f;

    [SerializeField]
    private float _interactableDistance = 2;
    private bool _foundInteractable = false;

    private bool _move;
    private bool _rotate;
    [SerializeField]
    private bool _isGrounded;

    private Quaternion _targetRotation;
    private Vector3 _pointposition;
    private Vector3 _transformposition;
    private Vector3 _targetDirection;
    private Vector3 _targetDirectionNormalize;
    private Vector3 _pointA;
    private Vector3 _pointB;
    private float _dist;
    private float _deltaAngle;
    private float _rotationKeel;
    private float _gravity = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _character.isGrounded;

        if (_isGrounded)
        {
            _gravity = 0;
        }
        else
        {
            _gravity = -9.8f;
            _character.Move(transform.up * _gravity * Time.deltaTime);

        }

        if (true)
        {
            RaycastHit hit;
            Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                _point.position = hit.point;
                _move = true;
                    // _rotate = true;
                _pointposition = _point.position;
                _pointposition.y = 0;
                _transformposition = transform.position;
                _transformposition.y = 0;
                _targetDirection = _point.position - transform.position;
                _targetDirection.y = 0.00F;

                transform.rotation = Quaternion.LookRotation(_targetDirection);

                if (_targetDirection.x * _targetDirection.x < _positionTrashHold)
                {
                    _targetDirection.x = 0;
                }
                if (_targetDirection.z* _targetDirection.z < _positionTrashHold)
                {
                    _targetDirection.z = 0;
                }

                _targetDirectionNormalize = _targetDirection.normalized;

                /*_dist = Vector3.Distance(_pointposition, _transformposition);
                _targetRotation = Quaternion.LookRotation(_targetDirection);
                _deltaAngle = Quaternion.Angle(transform.rotation, _targetRotation);
                _pointA = _transformposition - transform.forward * 2 * _dist;
                _pointB = _transformposition + transform.forward * 2 * _dist;
                _rotationKeel = (_pointB.x - _pointA.x) * (_pointposition.z - _pointA.z) - (_pointB.z - _pointA.z) * (_pointposition.x - _pointA.x);*/
            }
        }

        if (Input.GetMouseButton(0))
        {
            _castModel.CastUpdate();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _castModel.CastStop();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _castModel.AddSpheretoActive(SpheresElements.water.ToString());
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _castModel.AddSpheretoActive(SpheresElements.life.ToString());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _castModel.AddSpheretoActive(SpheresElements.shield.ToString());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _castModel.AddSpheretoActive(SpheresElements.freze.ToString());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _castModel.AddSpheretoActive(SpheresElements.razor.ToString());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _castModel.AddSpheretoActive(SpheresElements.dark.ToString());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _castModel.AddSpheretoActive(SpheresElements.earth.ToString());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            _castModel.AddSpheretoActive(SpheresElements.fire.ToString());
        }

        if (_rotate)
        {
            if(_deltaAngle > _angleTrashHold * _mouseSensitivity)
            {
                if (_rotationKeel > 0)
                {
                    _xRotation -= _mouseSensitivity * Time.deltaTime;
                }
                else
                {
                    _xRotation += _mouseSensitivity * Time.deltaTime;
                }

                transform.Rotate(Vector3.up * _xRotation);
                _deltaAngle = Quaternion.Angle(transform.rotation, _targetRotation);
            }
            else
            {
                _rotate = false;
            }
        }

        if(_move)
        {
            _character.Move(_speed * _targetDirectionNormalize * Time.deltaTime);

            if(_dist > _positionTrashHold)
            {
                _transformposition = transform.position;
                _transformposition.y = 0;
                _dist = Vector3.Distance(_pointposition, _transformposition);
            }
            else
            {
                _move = false;
            }
        }

    }

    public void OnMouseRight(InputAction.CallbackContext value)
    {
       //_mouse.x = value.ReadValue<float>();
        //Debug.Log(value);
    }

    public void OnMouseLookX(InputAction.CallbackContext value)
    {
        //_mouse.x = value.ReadValue<float>();
        //Debug.Log(value);
    }

    public void OnMouseLookY(InputAction.CallbackContext value)
    {
        //_mouse.y = value.ReadValue<float>();
        // Debug.Log(value);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        //_curMovement.MoveValue(value.ReadValue<Vector2>());
        if (value.phase.ToString().Equals("Started") || value.phase.ToString().Equals("Canceled"))
        {
            //Debug.Log(value.phase.ToString());

        }
        //
    }

    public void OnElement1(InputAction.CallbackContext value)
    {
        if (value.phase.ToString().Equals("Started"))
        {
            Debug.Log(value.phase.ToString());

        }

    }
    public void OnElement2(InputAction.CallbackContext value)
    {
        if (value.phase.ToString().Equals("Started"))
        {
            Debug.Log(value.phase.ToString());

        }

    }
    public void OnElement3(InputAction.CallbackContext value)
    {
        if (value.phase.ToString().Equals("Started"))
        {
            Debug.Log(value.phase.ToString());

        }

    }
    public void OnElement4(InputAction.CallbackContext value)
    {
        if (value.phase.ToString().Equals("Started"))
        {
            Debug.Log(value.phase.ToString());

        }

    }
    public void OnElement5(InputAction.CallbackContext value)
    {
        if (value.phase.ToString().Equals("Started"))
        {
            Debug.Log(value.phase.ToString());

        }

    }
    
    public void OnElement6(InputAction.CallbackContext value)
    {
        if (value.phase.ToString().Equals("Started"))
        {
            Debug.Log(value.phase.ToString());

        }

    }
    
    public void OnElement7(InputAction.CallbackContext value)
    {
        if (value.phase.ToString().Equals("Started"))
        {
            Debug.Log(value.phase.ToString());

        }

    }
    
    public void OnElement8(InputAction.CallbackContext value)
    {
        if (value.phase.ToString().Equals("Started"))
        {
            Debug.Log(value.phase.ToString());

        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.phase.ToString().Equals("Started"))
        {
            //_curMovement.JumpValue();
        }
    }

    public void OnFire(InputAction.CallbackContext value)
    {
        //Debug.Log(value.phase.ToString());
        if (value.phase.ToString().Equals("Started"))
        {
            //Debug.Log("fire");
            //_curGun.StartShooting();
            // _curMovement.JumpValue();
        }
        else if (value.phase.ToString().Equals("Canceled"))
        {
            //_curGun.StopShooting();
        }

        //_curMovement.JumpValue();
    }

    public void OnReloading(InputAction.CallbackContext value)
    {
        if (value.phase.ToString().Equals("Started"))
        {
            //_curGun.GunReloading();
        }
    }
}
