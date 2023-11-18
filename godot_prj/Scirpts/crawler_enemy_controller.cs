using Godot;
using System;

public partial class crawler_enemy_controller : RigidBody2D, killable
{
    float skitter_speed = 250.0f;
    int skitter_direction = 1;
    double skitter_direction_swap = 0.6d;

    public double damage_accessor = 40;

    level_handler parentHandler;

    public float health = 40f;

    const double inv_time = 0.26;
    public double dmg_cooldown = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        parentHandler = GetNode<level_handler>("../..");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        animatedSprite2D.Play("default");

        if (skitter_direction_swap <= 0)
        {
            Random rand = new Random();

            skitter_direction_swap = (rand.NextDouble() * 0.75) + 0.75f;
            skitter_direction *= -1;
            //Debug.Print("skatter");
        }

        LinearVelocity = new Vector2(skitter_speed * skitter_direction, LinearVelocity.Y);

        skitter_direction_swap -= delta;

        if (dmg_cooldown > 0)
        {
            dmg_cooldown -= delta;
        }
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
}
