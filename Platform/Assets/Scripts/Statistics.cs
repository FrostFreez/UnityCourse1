using UnityEngine;

public class Statistics
{
    private static Statistics instance;
    public static Statistics Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = new Statistics();
            return instance;
        }
        private set
        {

        }
    }
    public int furthersLevel = 0;
    public float[] fastestTime = new float[15];
    public float totalTime = 0;

    private Statistics()
    {
        for (int i = 0; i < fastestTime.Length; i++)
        {
            fastestTime[i] = float.MaxValue;
        }
    }
}
