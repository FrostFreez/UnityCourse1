using UnityEngine;
using System;

public class CoreComponent : MonoBehaviour, IComparable<CoreComponent>
{
    [SerializeField] protected EntityController controller;
    [SerializeField] public int order = 0;
    public void SetController(EntityController controller)
    {
        this.controller = controller;
    }
    public virtual void StartComponent() { }
    public virtual void UpdateComponent() { }

    public int CompareTo(CoreComponent other)
    {
        return order - other.order;
    }
}
