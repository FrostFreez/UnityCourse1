using System.Linq;
using UnityEngine;

public class StateMachine : CoreComponent
{
    public BaseState[] allStates;
    public BaseState state;
    public void Initialize(BaseState firstState)
    {
        state = firstState;
        state.Enter();
    }
    public void ChangeState(BaseState nextState)
    {
        state.Exit();
        state = nextState;
        state.Enter();
    }
    public void ChangeState(string nextState)
    {
        ChangeState(FindState(nextState));
    }
    public BaseState FindState(string stateName)
    {
        BaseState thisState = allStates.FirstOrDefault(x => x.stateName == stateName);

        if (thisState == null) { Debug.LogWarning(transform.parent.name + ": " + stateName + " not found"); }
        return thisState;
    }
    public override void StartComponent()
    {
        allStates = GetComponentsInChildren<BaseState>();
        foreach (BaseState s in allStates)
        {
            s.SetUp(controller, this);
        }
        Initialize(allStates[0]);
    }
    public override void UpdateComponent()
    {
        state.UpdateState();
    }
    public override void FixedUpdateComponent()
    {
        state.PhysicsUpdate();
    }
}
