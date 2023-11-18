using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class GripperEnemy : RigidBody2D, killable
{
	[Export]
	private Vector2 speed = new Vector2(0, 0);
	private CharacterBody2D player;
	private Vector2 target_pos;

	public float health = 50f;

	const double inv_time = 0.26;
	public double dmg_cooldown = 0;

	public const double damage_amount = 70;
	public double damage_accessor = damage_amount;

	level_handler parentHandler;
	Area2D dmg_area;

	CharacterBody2D characterBody;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<CharacterBody2D>("/root/GameRuntime/ProtagBody");
		target_pos = player.Position;

		parentHandler = GetNode<level_handler>("../..");
		characterBody = GetNode<CharacterBody2D>("../../../ProtagBody");
	}

    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		var targ_vec = Position.DirectionTo(target_pos) * 80;
		state.LinearVelocity = targ_vec;
		target_pos = player.Position;
	}

    public override void _Process(double delta)
    {
		if(dmg_cooldown > 0) 
		{
            dmg_cooldown -= delta;
        }
		
        base._Process(delta);
    }

	

    public void damage(float damage)
	{
		if(dmg_cooldown <= 0) 
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
}
