using com.czeeep.spell.biom;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private ModificatorZone _zone;
    [SerializeField]
    private string _elementFromFunction;
    [SerializeField]
    private int _powerFromFunction;
    [SerializeField]
    private string _element;
    [SerializeField]
    private int _power;
    [SerializeField]
    private bool _init = false;

   // Start is called before the first frame update
   void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            transform.position = transform.position + transform.forward * _speed * Time.deltaTime;

            if(!string.IsNullOrEmpty(_elementFromFunction))
            {
                _element = _elementFromFunction;
                _power = _powerFromFunction;
                _zone.UpdateInfo(_element, _power);
                _init = true;
            }
        }
        else
        {
            if (_init)
            {
                _zone.UpdateInfo(_element, _power);
                _init = false;
            }
        }

    }

    public void UpdateInfo(string element, int power) 
    {
        _elementFromFunction = element;
        _powerFromFunction = power;
    }


    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_element);
            stream.SendNext(_power);
            stream.SendNext(_init);
        }
        else
        {
            this._element = (string)stream.ReceiveNext();
            this._power = (int)stream.ReceiveNext();
            this._init = (bool)stream.ReceiveNext();
        }
    }
}
