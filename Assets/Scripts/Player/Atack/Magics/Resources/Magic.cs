using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected int _direction;
    [SerializeField]
    protected int _power;
    [SerializeField]
    protected bool _shouldDestroy;

    protected PhotonView _photonView;
    /*
    [SerializeField]
    protected MagicInfo _magicInfo;*/
    /*[SerializeField]
    protected PlayerNetworkView _view;*/

    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();

        object[] instantiationData = _photonView.InstantiationData;
        _name = (string)instantiationData[0];
        _direction = (int)instantiationData[1];
        _power = (int)instantiationData[2];

        gameObject.name = $"Magic {_name}: {(CastDirection)_direction} - {_power}";

        if(photonView.IsMine)
        {
            PlayerNetworkView.LocalStaffModelInstance.AddMagic(_name, this);
        }
        /* _view = transform.GetComponentInParent<PlayerNetworkView>();
         _view.AddMagic(gameObject);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.zero;
        }

        if (_shouldDestroy)
        {
            Destroy(gameObject);
        }

        //Debug.Log($"cast {_magicInfo.name}: {_magicInfo.power}");
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {

        /*object[] instantiationData = info.photonView.InstantiationData;
        _name = (string)instantiationData[0];
        _direction = (int)instantiationData[1];
        _power = (int)instantiationData[2];

        //_magicInfo = new MagicInfo((string)instantiationData[0], (CastDirection)instantiationData[1], (int)instantiationData[2]);

        PlayerNetworkView.LocalStaffModelInstance.AddMagic(_name, this);*/
        // ...
    }

    public void Init()
    {
    }

    public void UpdateInfo(string name, int direction, int power)
    {
        _name = name;
        _direction = direction;
        _power = power;
        //_magicInfo = magicInfo;
        gameObject.name = $"Magic {_name}: {(CastDirection)_direction} - {_power}";
    }

    public void DestroyMagic()
    {
        _shouldDestroy = true;
        //PhotonNetwork.Destroy(gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_name);
            stream.SendNext(_direction);
            stream.SendNext(_power);
            stream.SendNext(_shouldDestroy);

        }
        else
        {
            this._name = (string)stream.ReceiveNext();
            this._direction = (int)stream.ReceiveNext();
            this._power = (int)stream.ReceiveNext();
            this._shouldDestroy = (bool)stream.ReceiveNext();
        }
    }
}
/*
[Serializable]
public class MagicInfo
{
    public string name;
    public CastDirection direction;
    public int power;

    public MagicInfo(string name, CastDirection direction, int power)
    {
        this.name = name;
        this.direction = direction;
        this.power = power;
    }

}*/
