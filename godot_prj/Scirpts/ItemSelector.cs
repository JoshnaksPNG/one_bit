using Godot;
using System;
using System.Diagnostics;

public partial class ItemSelector : TextureButton
{
	int index;
	TextureRect parentItem;
	Inventory inventory;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parentItem = GetNode<TextureRect>("..");
		index = (parentItem.Name.ToString()[4]) - 48;

		inventory = parentItem.GetNode<Inventory>("..");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Pressed()
    {
		inventory.setSelected(index);
        base._Pressed();
    }
}
