using Godot;
using System;
using System.Diagnostics;

public partial class protag_movement : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -900.0f;
    public const float DashVelocity = 900f;


	// Custom Movement Variables
	private bool hasDoubleJump = false;
	private const float doubleJumpRatio = (2f / 3f);

    const double DASH_DURATION = 0.3d;
    double dash_timer = DASH_DURATION;
    bool is_dashing = false;
    int dash_direction = 0;
    bool can_dash = true;


	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Level Manager Script
	LevelManager lvl_manager;

    public override void _Ready()
    {
        lvl_manager = GetNode<LevelManager>("../LevelManager");



        // base._Ready();
    }

    public override void _PhysicsProcess(double delta)
	{
        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        if (!lvl_manager.is_changing_level) // Controll Normally if Level is Not Changing
        {
            Vector2 velocity = Velocity;

            if (!is_dashing)
            {
                
                Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

                // Add the gravity and animations
                if (!IsOnFloor())
                {
                    velocity.Y += gravity * (float)delta;
                    animatedSprite2D.Play("jump");
                }
                else
                {
                    // Add Double Jump and Dash Back If Touching Ground
                    hasDoubleJump = true;
                    can_dash = true;

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
                    else if (hasDoubleJump)
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

                // Handle Dashing
                if (can_dash)
                {
                    bool movingLeft = direction.X < 0;

                    if (Input.IsActionJustPressed("dash_left") || (Input.IsActionJustPressed("dash") && movingLeft))
                    {
                        dash_direction = -1;
                        is_dashing = true;
                        can_dash = false;
                    }
                    else if (Input.IsActionJustPressed("dash_right") || (Input.IsActionJustPressed("dash") && !movingLeft))
                    {
                        dash_direction = 1;
                        is_dashing = true;
                        can_dash = false;
                    }
                    else
                    {
                        dash_direction = 0;
                        dash_timer = DASH_DURATION;
                    }
                }
                
            }
            else // If Character is Dashing
            {
                if (dash_timer <= 0)
                {
                    is_dashing = false;
                }
                else
                {
                    velocity.X = DashVelocity * dash_direction;
                    velocity.Y = 0;

                    dash_timer -= delta;
                }
            }

            Velocity = velocity;
            MoveAndSlide();
        }
        else // If Level Is Changing
        {
            animatedSprite2D.Play("walk");
            animatedSprite2D.FlipH = false;
        }
	}
}
