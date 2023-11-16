using Godot;
using System;

public partial class GripperEnemy : RigidBody2D
{
	[Export]
	private Vector2 mov = new Vector2(1, -2);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		state.AddConstantForce(mov);
	}
}
