using System.Collections.Generic;
using UnityEngine;

public abstract class BallSO : ScriptableObject
{
    public List<Status> baseStatus;
    public Sprite sprite;
    public float this[StatusType type]
    {
        get
        {
            return baseStatus.Find(x => x.type == type).value;
        }
        set
        {
            baseStatus.Find(x => x.type == type).value = value;
        }
    }
    public abstract void Pressed(Ball ball);
    public abstract void Released(Ball ball);
}
