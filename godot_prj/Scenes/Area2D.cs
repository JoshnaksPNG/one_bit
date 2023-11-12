using Godot;
using System;
using System.Diagnostics;

public partial class Area2D : Godot.Area2D
{
	//Node2D level;
	level_handler lhandler;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//level = GetNode<Node2D>(".");
        lhandler = GetNode<level_handler>("..");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		bool overlap = HasOverlappingBodies();
		
		if (overlap) 
		{
			//((level_handler)level.GetScript()).decrementEnemies();
			lhandler.decrementEnemies();
		}
	}
}
