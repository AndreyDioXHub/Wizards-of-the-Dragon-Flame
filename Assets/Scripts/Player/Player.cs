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
    private Transform _point;

   [SerializeField]
    private Transform _playerCamera;
    [SerializeField]
    private float _mouseSensitivity = 100f;
    [SerializeField]
    private float _speed;

    private float _xRotation = 0f;
    private float _angleTrashHold = 1f;
    private float _positionTrashHold = 0.1f;

    [SerializeField]
    private float _interactableDistance = 2;
    private bool _foundInteractable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Rotate")]
    public void RotateToPoint()
    {
        StartCoroutine(RotateToPointCoroutine());
    }

    public IEnumerator RotateToPointCoroutine()
    {
        _xRotation = 0;

        var targetDirection = _point.position - transform.position;

        targetDirection.y = 0.00F;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        float deltaAngle = Quaternion.Angle(transform.rotation, targetRotation);

        var pointposition = _point.position;
        pointposition.y = 0;
        var transformposition = transform.position;
        transformposition.y = 0;
        var dist = Vector3.Distance(pointposition, transformposition);
        var pointA = transformposition - transform.forward * 2 * dist;
        var pointB = transformposition + transform.forward * 2 * dist;
        var rotationKeel = (pointB.x - pointA.x) * (pointposition.z - pointA.z) - (pointB.z - pointA.z) * (pointposition.x - pointA.x);

        while (deltaAngle > _angleTrashHold * _mouseSensitivity)
        {
            yield return new WaitForEndOfFrame();

            if (rotationKeel > 0)
            {
                _xRotation -= _mouseSensitivity * Time.deltaTime;
            }
            else
            {
                _xRotation += _mouseSensitivity * Time.deltaTime;
            }

            transform.Rotate(Vector3.up * _xRotation);
            deltaAngle = Quaternion.Angle(transform.rotation, targetRotation);

        }
    }

    [ContextMenu("Move")]
    public void MoveToPoint()
    {
        StartCoroutine(MoveToPointCoroutine());
    }

    public IEnumerator MoveToPointCoroutine()
    {
        var pointposition = _point.position;
        pointposition.y = 0;
        var transformposition = transform.position;
        transformposition.y = 0;
        var targetDirection = _point.position - transform.position;
        targetDirection.y = 0.00F;
        var targetDirectionNormalize = targetDirection.normalized;
        var dist = Vector3.Distance(pointposition, transformposition);

        _character.Move(_speed * targetDirectionNormalize * Time.deltaTime);

        while (dist > _positionTrashHold)
        {
            yield return new WaitForEndOfFrame();
            
            _character.Move(_speed * targetDirectionNormalize * Time.deltaTime);

            transformposition = transform.position;
            transformposition.y = 0;
            dist = Vector3.Distance(pointposition, transformposition);
        }

        yield return null;
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
