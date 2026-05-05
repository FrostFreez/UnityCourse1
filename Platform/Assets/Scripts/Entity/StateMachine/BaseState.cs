using UnityEngine;
using System;

[Serializable]
public abstract class BaseState
{
    public BaseState(EntityController controller, StateMachine stateMachine, string animString)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;
        this.animString = animString;
    }
    [field: HideInInspector] protected EntityController controller { get; private set; }
    [field: HideInInspector] protected StateMachine stateMachine { get; private set; }
    [field: SerializeField] protected float enterTime { get; private set; }
    [field: SerializeField] protected string animString { get; private set; }
    public virtual void Enter() { enterTime = Time.time; DoChecks(); controller.anim.SetBool(animString, true); }
    public virtual void DoChecks() { }
    public virtual void Update() { DoChecks(); }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { controller.anim.SetBool(animString, false); }
}