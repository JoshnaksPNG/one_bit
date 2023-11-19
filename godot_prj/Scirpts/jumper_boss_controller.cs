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
	bool should_jump = true;
	float jump_val = 0;

	MotionMode mode = MotionMode.Still;

	MotionMode[] modes = new MotionMode[3] { MotionMode.Still, MotionMode.Skitter, MotionMode.Jump };

	public double damage_accessor = 500;

    level_handler parentHandler;
	CurrencyCounter counter;

    public float health = 250f;

    const double inv_time = 0.12;
    public double dmg_cooldown = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		parentHandler = GetNode<level_handler>("../..");
		counter = GetNode<CurrencyCounter>("../../../CurrencyCounter");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if( mode_timer <= 0 ) 
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
				//LinearVelocity = new Vector2 (0, LinearVelocity.Y);

				break;

			case MotionMode.Jump:

				if (LinearVelocity.Y == 0)
				{
					if (jump_still_timer <= 0)
					{
						Random rand = new Random();
						//jump((rand.Next(1800) - 900));
						jump_val = rand.Next(1800) - 900;
						should_jump = true;
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

				//LinearVelocity = new Vector2 ( skitter_speed * skitter_direction, LinearVelocity.Y);

				skitter_direction_swap -= delta;
				break;
		}

		mode_timer -= delta;
        if (dmg_cooldown > 0)
        {
            dmg_cooldown -= delta;
        }
    }

    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        switch (mode)
        {
            case MotionMode.Still:
                state.LinearVelocity = new Vector2(0, state.LinearVelocity.Y);

                break;

            case MotionMode.Jump:

				if (should_jump)
				{
					jump(jump_val, state);
					should_jump = false;
				}
                break;

            case MotionMode.Skitter:
                state.LinearVelocity = new Vector2(skitter_speed * skitter_direction, state.LinearVelocity.Y);
                break;
        }

        base._IntegrateForces(state);
    }


    void jump(double x, PhysicsDirectBodyState2D state)
	{

		Vector2 jumpDirection = new Vector2((float)x, jump_velocity);
		state.LinearVelocity= jumpDirection;
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
		counter.addCurrency(500);
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
