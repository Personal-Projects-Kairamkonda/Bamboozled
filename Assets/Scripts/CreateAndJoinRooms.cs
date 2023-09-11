using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
  

    private void Start()
    {
        
    }


    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(Constants.gameScene);
    }

}


