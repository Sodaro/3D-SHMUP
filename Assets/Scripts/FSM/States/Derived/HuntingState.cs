using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingState : State
{
	public HuntingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
	{
	}

	public override void Enter()
	{
		//Debug.Log($"State entered: {this}");
		base.Enter();
		//_stateMachine.ChangeState(new DormantState(_enemy, _stateMachine));
	}

	public override void Exit()
	{
		//Debug.Log($"State exited: {this}");
		base.Exit();
	}

	public override void HandleInput()
	{
		base.HandleInput();
	}

	public override void Update()
	{
		base.Update();
		_enemy.RotateTowardsTarget();
		_enemy.MoveTowardsTarget();
	}
}
