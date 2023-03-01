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

    private PhotonView _pview;
    /*[SerializeField]
    private float _speed = 20;*/
    /*[SerializeField]
    private float _timeToDestroy = 10f;*/
    /*[SerializeField]
    private float _gravity = -9.8f;*/

    private string _elementFromFunction;
    private int _powerFromFunction;
    private string _element;
    private int _power;

    private bool _initFromFunction = false;
    private bool _init = false;
    private bool _destroyAfterTimeEnd = true;
    private float _time = 0;
    private float _nexttime = 0;

   // private Vector3 _direction;
    private Vector3 _origin;

    private Vector3 _position;
    private Vector3 _nextPosition;

    private Vector3 _bulletDirection;
    private float _bulletDeltaPath = 0;

    private bool _isGrounded;
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

        _position = _origin + _info.length * transform.forward * _time + Vector3.up * _info.gravity * _time * _time / 2f;
        _nextPosition = _origin + _info.length * transform.forward * _nexttime + Vector3.up * _info.gravity * _nexttime * _nexttime / 2f;
        _bulletDirection = (_nextPosition - _position).normalized;
        _bulletDeltaPath = Vector3.Distance(_nextPosition, _position);

        _bulletDirection = (_nextPosition - _position).normalized;
        _bulletDeltaPath = Vector3.Distance(_nextPosition, _position);

        if (Physics.Raycast(_position, _bulletDirection, out RaycastHit hit, _bulletDeltaPath))
        {
            if (hit.collider.tag.Equals("Ground"))
            {
                Debug.DrawRay(_position, _bulletDirection * _bulletDeltaPath, Color.white);

                _isGrounded = true;

                if (_destroyAfterGrounding)
                {
                    CallDestroyProjectile();
                }
            }
            else
            {
                Debug.DrawRay(_position, _bulletDirection * _bulletDeltaPath, Color.red);
            }
        }
        else
        {
            Debug.DrawRay(_position, _bulletDirection * _bulletDeltaPath, Color.white);
        }

        if (photonView.IsMine)
        {
            if (!_isGrounded)
            {
                transform.position = _position;
            }

            if (!string.IsNullOrEmpty(_elementFromFunction) && _initFromFunction)
            {
                Debug.Log("!string.IsNullOrEmpty(_elementFromFunction)");
                _element = _elementFromFunction;
                _power = _powerFromFunction;
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
        if (MagicConst.TYPE_PROJECTILE_BY_KEY.TryGetValue(_element, out ProjectileInfo value))
        {
            _info = value;
        }

        _info.length = _info.length / _power;

        _zone.UpdateInfo(_element, _power);
    }

    public void UpdateInfo(string element, int power) 
    {
        _elementFromFunction = element;
        _powerFromFunction = power;
        _initFromFunction = true;
        //_speed = _speed / power;

        //_tick.UpdateInfo(_timeToDestroy);

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
            stream.SendNext(_init);
            //stream.SendNext(_isGrounded);
        }
        else
        {
            this._element = (string)stream.ReceiveNext();
            this._power = (int)stream.ReceiveNext();
            this._init = (bool)stream.ReceiveNext();
            //this._isGrounded = (bool)stream.ReceiveNext();
        }
    }
}

[Serializable]
public class ProjectileInfo
{
    public float length;
    public float timeToDestroy;
    public float gravity;

    public ProjectileInfo()
    {
        this.length = 50f;
        this.timeToDestroy = 10f;
        this.gravity = -9.8f;
    }

    public ProjectileInfo(float length, float timeToDestroy, float gravity)
    {
        this.length = length;
        this.timeToDestroy = timeToDestroy;
        this.gravity = gravity;
    }
}
