using UnityEngine;
using System.Collections.Generic;
using System;

public class StatusBehaivour : MonoBehaviour
{
    [SerializeField] private List<Status> BaseStatus;
    [HideInInspector] public List<Status> baseStatus
    {
        get
        {
            return BaseStatus;
        }
        set
        {
            BaseStatus.Clear();
            status.Clear();
            for (int i = 0; i < value.Count; i++)
            {
                BaseStatus.Add(new() { type = value[i].type, value = value[i].value });
                status.Add(new() { type = value[i].type, value = value[i].value });
            }
        }
    }
    [SerializeField] public List<Status> status;

    private void Update()
    {
        for (int i = 0; i < baseStatus.Count; i++)
        {
            status[i].value = baseStatus[i].value;
        }
    }
    public float this[StatusType type]
    {
        get
        {
            return status.Find(x => x.type == type).value;
        }
        set
        {
            baseStatus.Find(x => x.type == type).value = value;
            updateStatus(type, value);
        }
    }
    public delegate void StatusChanged(StatusType type, float status);
    public StatusChanged updateStatus;
}

[Serializable]
public class Status
{
    public StatusType type = StatusType.HP;
    public float value = 0;
}

public enum StatusType
{
    HP,
    Charges,
    Gravity,
    Force,
    LauncherSpeed,
    LauncherMaxDistance
}

public enum StatusChangeType
{
    Addition,
    Multiplication,
    Absolute
}