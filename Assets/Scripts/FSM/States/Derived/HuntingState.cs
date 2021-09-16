using UnityEngine;

public class HuntingState : State
{
	public HuntingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
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
		_enemy.RotateTowardsTarget();
		_enemy.MoveTowardsTarget();

		if (_enemy.HasAllyInFront)
		{
			_enemy.StopShooting();
		}
		else
		{
			if (!_enemy.IsShooting)
				_enemy.StartShooting();
		}
	}
}
