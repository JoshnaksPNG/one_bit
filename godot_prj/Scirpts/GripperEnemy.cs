using Godot;
using System;

public partial class GripperEnemy : RigidBody2D
{
	[Export]
	private Vector2 speed = new Vector2(0, 0);
	private CharacterBody2D player;
	private Vector2 target_pos;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<CharacterBody2D>("/root/GameRuntime/ProtagBody");
		target_pos = player.Position;
	}
	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		// Vector2 mov = new Vector2();
		// mov.X = Mathf.MoveToward(Position.X, target_pos.X, speed.X);
		// mov.Y = Mathf.MoveToward(Position.Y, target_pos.Y, speed.Y);
		// state.AddConstantForce(mov);
		// target_pos = player.Position;
	}
}
