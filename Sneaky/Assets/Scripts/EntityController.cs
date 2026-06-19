using UnityEngine;
using System;

public class EntityController : MonoBehaviour
{
    [SerializeField] private CoreComponent[] coreComponents = { };
    public Rigidbody rb;
    public Animator anim;
    public virtual void Start()
    {
        coreComponents = GetComponentsInChildren<CoreComponent>();
        Array.Sort(coreComponents);
        foreach (CoreComponent component in coreComponents)
        {
            component.SetController(this);
            component.StartComponent();
        }
    }
    public virtual void Update()
    {
        foreach (CoreComponent component in coreComponents) component.UpdateComponent();
    }
    public void FixedUpdate()
    {
        foreach (CoreComponent component in coreComponents) component.FixedUpdateComponent();
    }
    public T FindCore<T>() where T : CoreComponent
    {
        foreach (CoreComponent c in coreComponents)
        {
            if (c is T ret)
            {
                return ret;
            }
        }
        Debug.LogWarning("No " + typeof(T) + " component was found!");
        return null;
    }
}
