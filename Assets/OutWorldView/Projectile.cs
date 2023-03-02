using com.czeeep.spell.biom;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private ModificatorZone _zone;
    [SerializeField]
    private ProjectileInfo _info = new ProjectileInfo();
    [SerializeField]
    private AnimationCurve _speedFallByCount;
    [SerializeField]
    private float _maxCount = 5f; 

    private PhotonView _pview;
    /*[SerializeField]
    private float _speed = 20;*/
    /*[SerializeField]
    private float _timeToDestroy = 10f;*/
    /*[SerializeField]
    private float _gravity = -9.8f;*/

    private string _elementFromFunction;
    private int _powerFromFunction;
    private int _speedPowerFromFunction;
    private string _element;
    private int _power;

    [SerializeField]
    private int _speedPower;
    [SerializeField]
    private float _vilosity = 0;
    [SerializeField]
    private Vector3 _gravityVilosity = Vector3.zero;

    private bool _initFromFunction = false;
    private bool _init = false;
    private float _time = 0;
    private float _nexttime = 0;

   // private Vector3 _direction;
    private Vector3 _origin;

    private Vector3 _position;

    private bool _isGrounded;

    [SerializeField]
    private bool _destroyAfterGrounding = true;


    // Start is called before the first frame update
    void Start()
    {
        _pview = PhotonView.Get(gameObject);

        if (photonView.IsMine)
        {
            _origin = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        _nexttime = _time + Time.deltaTime;

        _position = _info.speed * transform.forward * Time.deltaTime;

        _gravityVilosity.y += _info.gravity * Time.deltaTime * Time.deltaTime;

        _position += _gravityVilosity;

        _info.speed -= _vilosity * Time.deltaTime;

        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.5f))
        {
            if (hit.collider.tag.Equals("Ground"))
            {
                _isGrounded = true;

                if (_destroyAfterGrounding)
                {
                    CallDestroyProjectile();
                }
            }
        }

        if (photonView.IsMine)
        {
            if (!_isGrounded)
            {
                transform.position += _position;
            }

            if (!string.IsNullOrEmpty(_elementFromFunction) && _initFromFunction)
            {
                Debug.Log("!string.IsNullOrEmpty(_elementFromFunction)");
                _element = _elementFromFunction;
                _power = _powerFromFunction;
                _speedPower = _speedPowerFromFunction;

                Init();
                _initFromFunction = false;
                _init = true;
            }
        }
        else
        {
            if (_init)
            {
                Init();

                _init = false;
            }
        }
    }

    public void Init() 
    {
        /*
        if (MagicConst.TYPE_PROJECTILE_BY_KEY.TryGetValue(_element, out ProjectileInfo value))
        {
            _info = value;
        }*/
        _info.speed = _info.speed * _speedFallByCount.Evaluate(( _maxCount - _speedPowerFromFunction) / _maxCount);

        _vilosity = _info.speed / _info.timeToDestroy;
        _zone.UpdateInfo(_element, _power);
    }

    public void UpdateInfo(string element, int power, int speedPower) 
    {
        _elementFromFunction = element;
        _powerFromFunction = power;
        _speedPowerFromFunction = speedPower;
        _initFromFunction = true;
    }

    public void CallDestroyProjectile()
    {
        _pview.RPC("DestroyProjectile", RpcTarget.All);
    }

    [PunRPC]
    public void DestroyProjectile()
    {
        _zone.DestroyZone();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_element);
            stream.SendNext(_power);
            stream.SendNext(_speedPower);
            stream.SendNext(_init);
            //stream.SendNext(_isGrounded);
        }
        else
        {
            this._element = (string)stream.ReceiveNext();
            this._power = (int)stream.ReceiveNext();
            this._speedPower = (int)stream.ReceiveNext();
            this._init = (bool)stream.ReceiveNext();
            //this._isGrounded = (bool)stream.ReceiveNext();
        }
    }
}

[Serializable]
public class ProjectileInfo
{
    public float speed;
    public float timeToDestroy;
    public float gravity;

    public ProjectileInfo()
    {
        this.speed = 50f;
        this.timeToDestroy = 10f;
        this.gravity = -9.8f;
    }

    public ProjectileInfo(float speed, float timeToDestroy, float gravity)
    {
        this.speed = speed;
        this.timeToDestroy = timeToDestroy;
        this.gravity = gravity;
    }
}
