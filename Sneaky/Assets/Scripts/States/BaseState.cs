using UnityEngine;
using System;

[Serializable]
public abstract class BaseState : MonoBehaviour
{
    public virtual void SetUp(EntityController controller, StateMachine stateMachine)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;
    }
    protected EntityController controller { get; private set; }
    protected StateMachine stateMachine { get; private set; }
    protected float enterTime { get; private set; }
    [field: SerializeField] protected string animString { get; private set; }
    [field: SerializeField] public string stateName { get; private set; }
    public virtual void Enter() { enterTime = Time.time; DoChecks(); controller.anim.SetBool(animString, true); Debug.Log(stateName); }
    public virtual void DoChecks() { }
    public virtual void UpdateState() { DoChecks(); }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { controller.anim.SetBool(animString, false); }
}