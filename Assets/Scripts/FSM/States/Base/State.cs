using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{

	protected StateMachine _stateMachine;
	protected Enemy _enemy;

	public virtual void HandleInput() { }
    public virtual void Enter() 
	{ 
		//Debug.Log($"State entered: {this}");
	}
    public virtual void Exit()
	{
		//Debug.Log($"State exited: {this}");
	}
	public virtual void Update() { }
	//protected StateMachine stateMachine;

	protected State(Enemy enemy, StateMachine stateMachine)
	{
		_enemy = enemy;
		_stateMachine = stateMachine;
	}
}
