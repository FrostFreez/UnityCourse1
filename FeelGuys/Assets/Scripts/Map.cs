using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "Map", order = 0)]
public class Map : ScriptableObject
{
    public GameObject map;
    public Vector3 start;
    public Vector3 end;
}
