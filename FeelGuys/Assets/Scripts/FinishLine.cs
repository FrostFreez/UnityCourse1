using UnityEngine;
using Photon.Pun;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView pv = other.GetComponentInParent<PhotonView>();
            if (pv != null & pv.IsMine & !GameController.instance.playersWhoWon.Contains(pv.Owner))
            {
                GameController.instance.pv.RPC("PlayerWon", RpcTarget.All, pv.Owner, Statistics.instance.time);
            }
        }
    }
}
