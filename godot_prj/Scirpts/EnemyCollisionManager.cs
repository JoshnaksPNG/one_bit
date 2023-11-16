using Godot;
using System;

public partial class EnemyCollisionManager : Area2D
{
	// Called when the node enters the scene tree for the first time.
	private Node manager;
	public override void _Ready()
	{
		manager = GetNode("/root/GameRuntime/CurrencyCounter");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnAreaEntered(Godot.Area2D area)
	{
		var p = area.GetParent<RigidBody2D>();
		if (p.GetGroups().Contains("Enemy"))
		{
			GD.Print("fuck me");
			manager.Call("addCurrency", -10);
		}
		
	}
}
