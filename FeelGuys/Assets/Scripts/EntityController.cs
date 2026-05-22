using UnityEngine;
using System;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class EntityController : MonoBehaviour
{
    [SerializeField] private CoreComponent[] coreComponents = { };
    public StateMachine sm = new();
    public Rigidbody rb;
    public Animator anim;
    public PhotonView pv;

    public virtual void Start()
    {
        pv = gameObject.GetPhotonView();
        if (pv.IsMine)
        {
            coreComponents = GetComponentsInChildren<CoreComponent>();
            Array.Sort(coreComponents);
            foreach (CoreComponent component in coreComponents)
            {
                component.SetController(this);
                component.StartComponent();
            }
        }
    }
    public virtual void Update()
    {
        if (pv.IsMine)
        {
            foreach (CoreComponent component in coreComponents) component.UpdateComponent();
            sm.state?.Update();
        }
    }
    public void FixedUpdate()
    {
        if (pv.IsMine)
        {
            sm.state?.PhysicsUpdate();
        }
    }
    public T FindComponent<T>() where T : CoreComponent
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
