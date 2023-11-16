using Godot;
using Godot.Collections;
using System;

public partial class EnemyCatalogue : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//all_enemies.Add();
	}

	// Dictionary Containing All Enemy Types
    public static Dictionary<string, PackedScene> all_enemies = new Dictionary<string, PackedScene>() 
	{
		{ "gripper", ResourceLoader.Load<PackedScene>("res://Scenes/gripper_enemy.tscn") }
	};
}
