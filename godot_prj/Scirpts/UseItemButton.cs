using Godot;
using System;

public partial class UseItemButton : TextureButton
{
	Inventory inventory;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		inventory = GetNode<Inventory>("..");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Pressed()
    {
		inventory.useItem(inventory.current_item);
        base._Pressed();
    }
}
