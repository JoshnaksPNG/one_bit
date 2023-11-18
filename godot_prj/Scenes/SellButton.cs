using Godot;
using System;

public partial class SellButton : TextureButton
{
	Inventory inventory;
	LevelManager levelManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		inventory = GetNode<Inventory>("..");

		levelManager = GetNode<LevelManager>("../../LevelManager");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Visible = levelManager.is_village;
	}

    public override void _Pressed()
    {
		if(Visible) 
		{
            inventory.sellItem(inventory.current_item);
        }
        
        base._Pressed();
    }
}
