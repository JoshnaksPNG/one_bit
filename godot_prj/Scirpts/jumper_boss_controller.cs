using Godot;
using System;
using System.Diagnostics;
using System.Transactions;

public partial class jumper_boss_controller : RigidBody2D, killable
{
	float skitter_speed = 450.0f;

	const double skitter_timer = 10d;
	const double still_timer = 5d;
	const double jump_timer = 13d;
	const double jump_still = 0.3d;

	double mode_timer = still_timer;
	double jump_still_timer = jump_still;
	int skitter_direction = 1;
	double skitter_direction_swap = 0.3d;

	const float jump_velocity = -1000f;

	MotionMode mode = MotionMode.Still;

	MotionMode[] modes = new MotionMode[3] { MotionMode.Still, MotionMode.Skitter, MotionMode.Jump };

	public double damage_accessor = 150;

    level_handler parentHandler;

    public float health = 250f;

    const double inv_time = 0.12;
    public double dmg_cooldown = 0;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		parentHandler = GetNode<level_handler>("../..");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if ( mode_timer <= 0 ) 
		{
			Random rnd = new Random();
			MotionMode newMode;

			do
			{
				newMode = modes[rnd.Next(modes.Length)];
			} while (mode == newMode);

			mode = newMode;
			switch (mode) 
			{
                case MotionMode.Still:
					mode_timer = still_timer;
                    break;

                case MotionMode.Jump:
					mode_timer = jump_timer;
                    break;

                case MotionMode.Skitter:
					mode_timer = skitter_timer;
                    break;
            }
		}

		switch (mode) 
		{
			case MotionMode.Still:
				LinearVelocity = new Vector2 (0, LinearVelocity.Y);

				break;

			case MotionMode.Jump:

				if (LinearVelocity.Y == 0)
				{
					if (jump_still_timer <= 0)
					{
						Random rand = new Random();
						jump((rand.Next(1800) - 900));
						//Debug.Print("jamp");
					}


					jump_still_timer -= delta;
				}
				else 
				{
					
					jump_still_timer = jump_still;
				}
				break;

			case MotionMode.Skitter:
				if (skitter_direction_swap <= 0)
				{
					Random rand = new Random();

					skitter_direction_swap = (rand.NextDouble() * 0.5) + 0.5f;
					skitter_direction *= -1;
					//Debug.Print("skatter");
				}

				LinearVelocity = new Vector2 ( skitter_speed * skitter_direction, LinearVelocity.Y);

				skitter_direction_swap -= delta;
				break;
		}

		mode_timer -= delta;
        if (dmg_cooldown > 0)
        {
            dmg_cooldown -= delta;
        }

		//LinearVelocity = new Vector2(LinearVelocity.X, gravity * (float)delta);
    }

	void jump(double x)
	{

		Vector2 jumpDirection = new Vector2((float)x, jump_velocity);
		LinearVelocity= jumpDirection;
	}

    public void damage(float damage)
    {
        if (dmg_cooldown <= 0)
        {
            health -= damage;
            dmg_cooldown = inv_time;
        }

        if (health <= 0)
        {
            this.kill();
        }

    }

    public void kill()
    {
        parentHandler.decrementEnemies();
        this.QueueFree();
    }

    enum MotionMode 
	{
		Still,
		Skitter,
		Jump,
	}
}
