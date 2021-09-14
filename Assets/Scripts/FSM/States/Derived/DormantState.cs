using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DormantState : State
{
	public DormantState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		_enemy.StopShooting();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void HandleInput()
	{
		base.HandleInput();
	}

	public override void Update()
	{
		base.Update();
	}
}
