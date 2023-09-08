using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using StarterAssets;

public class PlayerSync : MonoBehaviourPunCallbacks
{

    [Header("Player Properties")]
    [SerializeField] private PlayerProperties playerProperties;

    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        // Initialize player properties
        playerProperties = new PlayerProperties();
        playerProperties.playerID = photonView.ViewID;
        playerProperties.playerName = "Player" + photonView.ViewID;
        playerProperties.playerScore = 0;
        transform.GetComponent<TextMeshPro>().text = playerProperties.playerID.ToString();
    }



    public void IsLocalPlayer()
    {
        playerProperties.thirdPersonController.enabled = true;
        playerProperties.camera.SetActive(true);
        playerProperties.followCame.SetActive(true);
    }
    
    private void Update()
    {
        if (photonView.IsMine)
        {
            // Update player properties locally for the owning player
            // Example: playerProperties.playerScore += 1;

            // Sync the updated properties to other players
            photonView.RPC("UpdatePlayerProperties", RpcTarget.All, playerProperties);
        }
    }

    [PunRPC]
    private void UpdatePlayerProperties(PlayerProperties updatedProperties)
    {
        // Update player properties for all players
        playerProperties = updatedProperties;

        // You can access and use the updated properties here
        Debug.Log("Player Name: " + playerProperties.playerName);
        Debug.Log("Player Score: " + playerProperties.playerScore);
    }
}

[System.Serializable]
public class PlayerProperties
{
    public ThirdPersonController thirdPersonController;
    [Space]
    public GameObject camera;
    [Space]
    public GameObject followCame;
    [Space]
    public int playerID;
    public string playerName;
    public int playerScore;
    

    // Add more properties as needed
}
