using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [Header("Sever settings")]
    public SettingsData createSettings;
    public SettingsData joinSettings;

    private void Start()
    {
        createSettings.button.onClick.AddListener(() => CreateRoom());
        joinSettings.button.onClick.AddListener(() => JoinRoom());
    }

    public void CreateRoom()
    {
        Debug.Log("Room Created");
        PhotonNetwork.CreateRoom(createSettings.inputField.text);
    }

    public void JoinRoom()
    {
        Debug.Log("Joined Room");
        PhotonNetwork.JoinRoom(joinSettings.inputField.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(Constants.gameScene);
    }

}

[System.Serializable]
public class SettingsData
{
    public Button button;
    public InputField inputField;
}

