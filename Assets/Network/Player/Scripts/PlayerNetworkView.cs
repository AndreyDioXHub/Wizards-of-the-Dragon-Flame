using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkView : MonoBehaviourPunCallbacks, IPunObservable
{
    public static StaffModel LocalStaffModelInstance;

    /*
    #region Staff 
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
    #endregion*/

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            LocalStaffModelInstance = StaffModel.Instance;
            //StaffModel.Instance.OnStaffShoot.AddListener(OnStaffShoot);
            //StaffModel.Instance.OnStaffShootStop.AddListener(OnStaffShootStop);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (_executeShoot)
        {

            if (_isShoot)
            {
                if (_magicInited)
                {


                }
                else
                {
                    if(_spheresCount.Count > 0)
                    {
                        foreach (var sphC in _spheresCount)
                        {
                            GameObject go = Instantiate(Resources.Load<GameObject>(_magicsList[sphC.Key]), transform);

                            Magic magic = go.GetComponent<Magic>();
                            magic.UpdateInfo(new MagicInfo(sphC.Key, _direction, sphC.Value));

                            _magics.Add(sphC.Key, magic);
                        }

                        _magicInited = true;
                    }
                    else
                    {
                        Debug.Log("chunk to the head");
                    }

                }
            }
            else
            {

            }

            _executeShoot = false;
        }*/

        /*if (photonView.IsMine)
        {
            //Destroy(_character);
        }
        else
        {
            return;
        }*/

        //_random = Random.Range(-1000, 1000);

    }



    public void OnStaffShoot(Dictionary<string, int> spheresCount, CastDirection direction)
    {
        //_spheresCount = spheresCount;
        //_direction = direction;
    }

    public void OnStaffShootStop(bool isShoot)
    {
        //_isShoot = isShoot;
       // _executeShoot = true;
    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*
        if (stream.IsWriting)
        {
            //We own this player: send the others out data
            //stream.SendNext(_random);
            stream.SendNext(_magicsList);
            stream.SendNext(_isShoot);
            stream.SendNext(_direction);
            stream.SendNext(_executeShoot);
        }
        else
        {
            //this._random = (int)stream.ReceiveNext();
            this._magicsList = (Dictionary<string, string>)stream.ReceiveNext();
            this._isShoot = (bool)stream.ReceiveNext();
            this._executeShoot = (bool)stream.ReceiveNext();
            this._direction = (CastDirection)stream.ReceiveNext();
        }*/
    }

    private void OnDestroy()
    {
        StaffModel.Instance.OnStaffShoot.RemoveListener(OnStaffShoot);
        StaffModel.Instance.OnStaffShootStop.RemoveListener(OnStaffShootStop);

    }
}
