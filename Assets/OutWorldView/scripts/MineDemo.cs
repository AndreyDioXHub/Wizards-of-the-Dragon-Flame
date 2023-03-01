using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && PhotonNetwork.IsMasterClient) {
            //PhotonNetwork.GetPhotonView()
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
