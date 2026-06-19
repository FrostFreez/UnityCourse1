using UnityEngine;
using System;
using Sirenix.OdinInspector;


public class CoreComponent : SerializedMonoBehaviour, IComparable<CoreComponent>
{
    [SerializeField] protected EntityController controller;
    [SerializeField] public int order = 0;
    public void SetController(EntityController controller)
    {
        this.controller = controller;
    }
    public virtual void StartComponent() { }
    public virtual void UpdateComponent() { }
    public virtual void FixedUpdateComponent() { }

    public int CompareTo(CoreComponent other)
    {
        return order - other.order;
    }
}
