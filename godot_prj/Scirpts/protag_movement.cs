using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public partial class protag_movement : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -900.0f;
    public const float DashVelocity = 900f;

    public const float Damage = 30f;
    public const float AttackSpeed = 0.2f;


	// Custom Movement Variables
	private bool hasDoubleJump = false;
	private const float doubleJumpRatio = (2f / 3f);

    const double DASH_DURATION = 0.3d;
    double dash_timer = DASH_DURATION;
    bool is_dashing = false;
    int dash_direction = 0;
    bool can_dash = true;

    bool was_flipped_last = true;

    // Modifiers
    public List<PlayerStatModifier> modifierList;

    float realSpeed = Speed;
    float realJumpVelocity = JumpVelocity;
    float realDashVelocity = DashVelocity;
    float realDJumpRatio = doubleJumpRatio;
    double realDashDuration = DASH_DURATION;
    float realDamage = Damage;
    float realAttackSpeed = AttackSpeed;


    CurrencyCounter currencyCounter;



	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Level Manager Script
	LevelManager lvl_manager;

    public override void _Ready()
    {
        lvl_manager = GetNode<LevelManager>("../LevelManager");

        currencyCounter = GetNode<CurrencyCounter>("../CurrencyCounter");

        modifierList = new List<PlayerStatModifier>();

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
                    animatedSprite2D.Play("suit_jump");

                    if (velocity.X != 0)
                    {
                        was_flipped_last = velocity.X >= 0;
                    }

                    animatedSprite2D.FlipH = was_flipped_last;
                }
                else
                {
                    // Add Double Jump and Dash Back If Touching Ground
                    hasDoubleJump = true;
                    can_dash = true;

                    if (velocity != Vector2.Zero)
                    {
                        animatedSprite2D.Play("suit_walk");
                        if (direction.X > 0)
                        {
                            animatedSprite2D.FlipH = true;
                            was_flipped_last = true;
                        }
                        else if (direction.X < 0)
                        {
                            animatedSprite2D.FlipH = false;
                            was_flipped_last = false;
                        }
                    }
                    else
                    {
                        animatedSprite2D.Play("suit_idle");
                        animatedSprite2D.FlipH = was_flipped_last;
                    }
                }


                // Handle Jump.
                if (Input.IsActionJustPressed("character_jump"))
                {
                    if (IsOnFloor())
                    {
                        velocity.Y = realJumpVelocity;
                    }
                    else if (hasDoubleJump)
                    {
                        hasDoubleJump = false;

                        float doubleJumpVelocity = realJumpVelocity * realDJumpRatio;

                        velocity.Y = doubleJumpVelocity < velocity.Y ? doubleJumpVelocity : velocity.Y;
                    }
                }


                // Get the input direction and handle the movement/deceleration.
                // As good practice, you should replace UI actions with custom gameplay actions.
                if (direction != Vector2.Zero)
                {
                    velocity.X = direction.X * realSpeed;
                }
                else
                {
                    velocity.X = Mathf.MoveToward(Velocity.X, 0, realSpeed);
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
                        dash_timer = realDashDuration;
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
                    animatedSprite2D.Play("suit_dash");
                    animatedSprite2D.FlipH = dash_direction > 0;
                    was_flipped_last = dash_direction > 0;

                    velocity.X = realDashVelocity * dash_direction;
                    velocity.Y = 0;

                    dash_timer -= delta;
                }
            }

            Velocity = velocity;
            MoveAndSlide();
            ModifyRealStats(delta);
        }
        else // If Level Is Changing
        {
            animatedSprite2D.Play("suit_walk");
            animatedSprite2D.FlipH = true;
            was_flipped_last = true;
        }
	}

    private void ModifyRealStats(double delta)
    {

        realSpeed = Speed;
        realJumpVelocity = JumpVelocity;
        realDamage = Damage;
        realAttackSpeed = AttackSpeed;
        realDashDuration = DASH_DURATION;
        realDashVelocity = DashVelocity;
        realDJumpRatio = doubleJumpRatio;

        for(int i = 0; i < modifierList.Count; i++) 
        {
            PlayerStatModifier current_modifier = modifierList[i];

            if (current_modifier.duration <= 0)
            {
                modifierList.RemoveAt(i);
                i--;
            }
            else 
            {
                switch (current_modifier.modifiedStat)
                {
                    case PlayerStatModifier.ModifiedStat.JumpVelocity:
                        {
                            realJumpVelocity += current_modifier.value;
                            break;
                        }
                    case PlayerStatModifier.ModifiedStat.DashVelocity:
                        {
                            realDashVelocity += current_modifier.value;
                            break;
                        }
                    case PlayerStatModifier.ModifiedStat.DoubleJumpRatio:
                        {
                            realDJumpRatio += current_modifier.value;
                            break;
                        }
                    case PlayerStatModifier.ModifiedStat.DashDuration:
                        {
                            realDashDuration += current_modifier.value;
                            break;
                        }
                    case PlayerStatModifier.ModifiedStat.Damage:
                        {
                            realDamage += current_modifier.value;
                            break;
                        }
                    case PlayerStatModifier.ModifiedStat.AttackSpeed:
                        {
                            realAttackSpeed += current_modifier.value;
                            break;
                        }
                    case PlayerStatModifier.ModifiedStat.Speed:
                        {
                            realSpeed += current_modifier.value;
                            break;
                        }
                    case PlayerStatModifier.ModifiedStat.Currency:
                        {
                            currencyCounter.addCurrency(current_modifier.value);
                            current_modifier.duration = 0;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                current_modifier.duration -= delta;
            }
        }
    }
}
