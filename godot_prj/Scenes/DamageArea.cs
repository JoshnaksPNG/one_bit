using Godot;
using Godot.Collections;
using System;

public partial class DamageArea : Area2D
{
    CharacterBody2D characterBody;
    protag_movement protag;

    GripperEnemy parent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        characterBody = GetNode<CharacterBody2D>("../../../../ProtagBody");
        protag = (protag_movement)characterBody;

        parent = GetNode<GripperEnemy>("..");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Array<Node2D> collided_areas = GetOverlappingBodies();

        if (collided_areas.Contains(characterBody))
        {
            protag.damage((float)parent.damage_accessor);
        }
    }
}
