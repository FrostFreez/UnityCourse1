using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    public List<BallSO> balls;
    public delegate void PointChange(int points);
    public static PointChange change;
    private static int Points;
    public static int points
    {
        get
        {
            return Points;
        }
        set
        {
            Points = value;
            change(Points);
        }
    }
}
