using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;

public class Dispenser : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnTime;
    [SerializeField] private float timeFromStart;
    [SerializeField] private int spawnsPerBurst;

    private void Update()
    {
        timeFromStart += Time.deltaTime;
        if (spawnTime < timeFromStart && PhotonNetwork.IsMasterClient)
        {
            timeFromStart = 0;

            if (spawnsPerBurst >= spawnPoints.Length)
            {
                foreach (Transform t in spawnPoints)
                {
                    Rigidbody rb = PhotonNetwork.Instantiate("Bomb", t.position, Quaternion.identity).GetComponentInChildren<Rigidbody>();
                    rb.linearVelocity = Vector3.up * -10;
                }
            }
            else
            {
                List<Transform> spawnPointsClone = new(spawnPoints);
                List<Transform> chosenSpawnPoints = new();

                for (int i = 0; i < spawnsPerBurst; i++)
                {
                    int index = Random.Range(0, spawnPointsClone.Count);
                    chosenSpawnPoints.Add(spawnPointsClone[index]);
                    spawnPointsClone.RemoveAt(index);
                }
                foreach (Transform t in chosenSpawnPoints)
                {
                    Rigidbody rb = PhotonNetwork.Instantiate("Bomb", t.position, Quaternion.identity).GetComponentInChildren<Rigidbody>();
                    rb.linearVelocity = Vector3.up * -10; 
                }
            }
        }
    }

}
