using Photon.Pun;
using UnityEngine;

public class DestroyItem : Killable
{
    public override void Kill()
    {
        PhotonNetwork.Destroy(transform.parent.gameObject.GetPhotonView());
    }
}
