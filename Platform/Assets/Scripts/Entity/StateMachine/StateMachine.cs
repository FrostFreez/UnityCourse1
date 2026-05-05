using System;

[Serializable]
public class StateMachine
{
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
}
