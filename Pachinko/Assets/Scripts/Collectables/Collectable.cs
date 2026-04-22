using UnityEngine;

public abstract class Collectable : ScriptableObject
{
    public abstract void Collect(Ball ball);
}
