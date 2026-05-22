using UnityEngine;

public class Statistics
{
    private static Statistics Instance;
    public static Statistics instance
    {
        get {
            Instance ??= new Statistics();
            return Instance;
        }
    }

    public float time;
    public int level = 1;

    public static string Seconds(float seconds, int totalCharacters)
    {
        string tTime = seconds.ToString().Trim();
        int j = 0;
        while (j < tTime.Length)
        {
            if (tTime[j] == '.')
            {
                j += 2;
                break;
            }
            j++;
        }
        if (j < totalCharacters - 1) j = totalCharacters - 1;
        return tTime.PadLeft(5)[..j] + "s";
    }
}
