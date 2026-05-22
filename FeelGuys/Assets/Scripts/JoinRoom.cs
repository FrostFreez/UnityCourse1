using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class JoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text playerName;

    public void JoinRoomM()
    {
        if (playerName != null)
        {
            PhotonNetwork.LocalPlayer.NickName = playerName.text.Trim();
        }
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("Gameplay");
    }
}
