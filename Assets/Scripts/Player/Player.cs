using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private CharacterController _character;

    [SerializeField]
    private Transform _playerCamera;
    [SerializeField]
    private float _mouseSensitivity = 100f;
    [SerializeField]
    private Vector2 _minMaxAngle;
    [SerializeField]
    private Vector2 _mouse;

    public float _xRotation = 0f;
    private float _yRotation = 0f;

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

    public void OnMouseLookX(InputAction.CallbackContext value)
    {
        _mouse.x = value.ReadValue<float>();
        //Debug.Log(value);
    }

    public void OnMouseLookY(InputAction.CallbackContext value)
    {
        _mouse.y = value.ReadValue<float>();
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
