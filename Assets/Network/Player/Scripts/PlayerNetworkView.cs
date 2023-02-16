using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkView : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private int _random;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!photonView.IsMine)
        {
            //Destroy(_character);
            return;
        }

        _random = Random.Range(-1000, 1000);

    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //We own this player: send the others out data
            stream.SendNext(_random);
        }
        else
        {
            this._random = (int)stream.ReceiveNext();
        }
    }
}
