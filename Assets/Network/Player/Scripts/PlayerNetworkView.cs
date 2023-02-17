using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkView : MonoBehaviourPunCallbacks, IPunObservable
{
    public static PlayerNetworkView LocalPlayerNetworkViewInstance;

    #region Staff 
    //public static GameObject LocalPlayerInstance;
    public static StaffModel LocalStaffModelInstance;

    [SerializeField]
    private List<GameObject> _magicsList = new List<GameObject>();
    /*
    private Dictionary<string, int> _spheresCount = new Dictionary<string, int>();
    [SerializeField]
    private CastDirection _direction;
    [SerializeField]
    private bool _isShoot;
    [SerializeField]
    private bool _executeShoot;

    [Tooltip("Non Network variable")]
    [SerializeField]
    private bool _magicInited;

    private Dictionary<string, string> _magicsList = new Dictionary<string, string>()
    {
        {"life","Magic" },
        {"fire","Magic" },
        {"water","Magic" },
        {"earth","Magic" },
        {"freze","Magic" },
        {"razor","Magic" },
        {"dark","Magic" },
        {"steam","Magic" },
        {"poison","Magic" },
        {"ice","Magic" },
        {"shield","Magic" },
    };
    */
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            LocalPlayerNetworkViewInstance = this;
            LocalStaffModelInstance = StaffModel.Instance;

            /*StaffModel.Instance.OnStaffShoot.AddListener(OnStaffShoot);
            StaffModel.Instance.OnStaffShootStop.AddListener(OnStaffShootStop);*/
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMagic(GameObject magic)
    {
        if (photonView.IsMine)
        {
            //_magicsList.Add(magic);
        }
    }

    public void OnStaffShoot(Dictionary<string, int> spheresCount, CastDirection direction)
    {
        //_spheresCount = spheresCount;
        //_direction = direction;
    }

    public void OnStaffShootStop(bool isShoot)
    {
        //_isShoot = isShoot;
        //_executeShoot = true;
    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //We own this player: send the others out data
            //stream.SendNext(_random);
            /*stream.SendNext(_magicsList);
            stream.SendNext(_isShoot);
            stream.SendNext(_direction);
            stream.SendNext(_executeShoot);*/
            //stream.SendNext(_magicsList);
        }
        else
        {
            //this._magicsList = (List<GameObject>)stream.ReceiveNext();
            //this._random = (int)stream.ReceiveNext();
            /*this._magicsList = (Dictionary<string, string>)stream.ReceiveNext();
            this._isShoot = (bool)stream.ReceiveNext();
            this._executeShoot = (bool)stream.ReceiveNext();
            this._direction = (CastDirection)stream.ReceiveNext();*/
        }
    }

    private void OnDestroy()
    {
        StaffModel.Instance.OnStaffShoot.RemoveListener(OnStaffShoot);
        StaffModel.Instance.OnStaffShootStop.RemoveListener(OnStaffShootStop);

    }
}
