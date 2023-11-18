using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class level_handler : Node2D
{
	const int collision_layer = 1;

	public int enemies = 0;
	Node2D doors;
	StaticBody2D door_collision;

	Node2D spawner_parent;

	List<Node2D> spawners = new List<Node2D>();

	Node2D enemy_tree;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		doors = GetNode<Node2D>("Doors");
		door_collision = GetNode<StaticBody2D>("Doors/DoorCollisions");

		spawner_parent = GetNode<Node2D>("./EnemySpawners");

		enemy_tree = GetNode<Node2D>("./Enemies");

		int child_count = spawner_parent.GetChildCount();
        for (int i = 0; i < child_count; i++)
		{
			spawners.Add(spawner_parent.GetChild<Node2D>(i));
		}
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
			clear_enemies();
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

	public void displayDoors(bool disp)
	{
		doors.Visible = disp;
		door_collision.SetCollisionLayerValue(collision_layer, disp);
	}

	public void instansiate_enemies(bool is_boss)
	{
		int spawner_count = spawners.Count;
		for (int i = 0; i < spawner_count; i++) 
		{
			RigidBody2D enemyBeingSpawned;

			Random rand = new Random();
			if (!is_boss)
			{
				List<PackedScene> enemyList = new List<PackedScene>(EnemyCatalogue.all_enemies.Values);
				//enemyBeingSpawned = EnemyCatalogue.all_enemies["gripper"].Instantiate<RigidBody2D>();
				enemyBeingSpawned = enemyList[rand.Next(enemyList.Count)].Instantiate<RigidBody2D>();
			}
			else
			{
				enemyBeingSpawned = EnemyCatalogue.all_bosses["jumper"].Instantiate<RigidBody2D>();
			}
			

			enemyBeingSpawned.Position = spawners[i].Position;
			enemy_tree.AddChild(enemyBeingSpawned);
			enemies++;
		}

		setEnemies(spawner_count);
	}

	public void clear_enemies()
	{
        Godot.Collections.Array<Node> enemy_list = enemy_tree.GetChildren();

		for (int i = 0; i < enemy_list.Count; i++)
		{
			enemy_list[i].QueueFree();
		}
	}
}
