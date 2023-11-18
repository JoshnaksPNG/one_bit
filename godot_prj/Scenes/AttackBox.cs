using Godot;
using System;

public partial class AttackBox : Area2D
{
	public bool is_active = false;

	public float damage = 30f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(is_active) 
		{
            Godot.Collections.Array<Node2D> bodies = this.GetOverlappingBodies();

			if(bodies != null) 
			{
				foreach(Node2D node in bodies) 
				{
					if (node is killable)
					{
						((killable)node).damage(damage);
					}
				}
			}
		}

	}

	//public
}
