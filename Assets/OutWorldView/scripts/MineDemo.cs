using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDemo : MonoBehaviour
{

    PhotonView pview;
    // Start is called before the first frame update
    void Start()
    {
        pview = PhotonView.Get(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && pview.IsMine) {
            PhotonNetwork.Destroy(gameObject);
            
        }
    }
}
