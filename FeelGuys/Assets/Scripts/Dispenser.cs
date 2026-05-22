using Photon.Pun;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnTime;
    [SerializeField] private float timeFromStart;

    private void Update()
    {
        timeFromStart += Time.deltaTime;
        if (spawnTime < timeFromStart && PhotonNetwork.IsMasterClient)
        {
            timeFromStart = 0;

            Rigidbody rb = PhotonNetwork.Instantiate("Bomb", spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity).GetComponentInChildren<Rigidbody>();
            rb.linearVelocity = Vector3.up * -10;
        }
    }

}
