using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State _currentState;
    //public State CurrentState { get; private set; }

    public State CurrentState => _currentState;

    public void Setup(State startState)
	{
        _currentState = startState;
        _currentState.Enter();
	}

    public void ChangeState(State newState)
	{
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
	}
}
