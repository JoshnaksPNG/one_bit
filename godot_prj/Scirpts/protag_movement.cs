using Godot;
using System;
using System.Diagnostics;

public partial class protag_movement : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -900.0f;


	// Custom Movement Variables
	private bool hasDoubleJump = false;
	private const float doubleJumpRatio = (2f / 3f);

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
		else
		{
			// Add Double Jump Back If Touching Ground
			if (!hasDoubleJump)
			{
				hasDoubleJump = true;
			}


			if (velocity != Vector2.Zero)
			{
				animatedSprite2D.Play("walk");
				if (direction.X > 0)
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
		}


		// Handle Jump.
		if (Input.IsActionJustPressed("character_jump"))
		{
			if (IsOnFloor())
			{
                velocity.Y = JumpVelocity;
            }
			else if(hasDoubleJump)
			{
				hasDoubleJump = false;

				float doubleJumpVelocity = JumpVelocity * doubleJumpRatio;

				velocity.Y = doubleJumpVelocity < velocity.Y ? doubleJumpVelocity : velocity.Y;
			}
		}
			

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
