using Godot;
using System;

public partial class level_handler : Node2D
{
	const int collision_layer = 1;

	int enemies = 1;
	Node2D doors;
	StaticBody2D door_collision;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		doors = GetNode<Node2D>("Doors");
		door_collision = GetNode<StaticBody2D>("Doors/DoorCollisions");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void setEnemies(int enemies) 
	{
		this.enemies = enemies;
	}

	public void decrementEnemies()
	{
		if (enemies <= 0)
		{
			enemies = 0;

		}
		else
		{
			--enemies;
		}

		if (enemies == 0)
		{
			displayDoors(false);
		}
	}

	private void displayDoors(bool disp)
	{
		doors.Visible = disp;
		door_collision.SetCollisionLayerValue(collision_layer, disp);
	}
}
