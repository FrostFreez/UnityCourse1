using UnityEngine;
using Photon.Pun;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private Map[] allMaps;
    [SerializeField] private Map[] map;
    [SerializeField] private Map start;
    [SerializeField] private Map end;

    [SerializeField] private GameObject portal;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            map = new Map[Statistics.instance.level + 2];
            map[0] = start;
            map[^1] = end;
            for (int i = 1; i < map.Length - 1; i++)
            {
                map[i] = allMaps[Random.Range(0, allMaps.Length)];
            }
            Vector3 currentOffset = Vector3.zero;
            for (int i = 0; i < map.Length; i++)
            {
                if (i > 1 && i < map.Length - 1) PhotonNetwork.Instantiate(portal.name, currentOffset, Quaternion.identity);
                currentOffset -= map[i].start;
                PhotonNetwork.Instantiate(map[i].map.name, currentOffset, Quaternion.identity);
                currentOffset += map[i].end;
            }
        }
    }
}
