using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class StartLine : MonoBehaviour
{
    [SerializeField] private GameObject bounderies;
    private void Start()
    {
        GameController.instance.changedStage += ChangeStage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView pv = other.GetComponentInParent<PhotonView>();
            if (pv != null & pv.Owner.IsMasterClient & GameController.instance.stage == GameStage.WaitForPlayers)
            {
                GameController.instance.pv.RPC("StartGame", RpcTarget.All);
            }
        }
    }
    private void ChangeStage(GameStage stage)
    {
        if (stage == GameStage.Game)
        {
            bounderies.SetActive(false);
        }
    }
}
