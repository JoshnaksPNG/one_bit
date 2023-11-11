using Godot;
using System;
using System.Diagnostics;

public partial class protag_movement : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		// Add the gravity and animations
		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
			animatedSprite2D.Play("jump");
		}
		else if (velocity != Vector2.Zero)
		{
			animatedSprite2D.Play("walk");
			if (direction.Angle() == 0)
			{
				animatedSprite2D.FlipH = false;
			}
			else
			{
				animatedSprite2D.FlipH = true;
			}
		}
		else
		{
			animatedSprite2D.Play("default");
		}
			
		// Handle Jump.
		if (Input.IsActionJustPressed("character_jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
