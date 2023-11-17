using Godot;
using System;

public partial class ChoiceSelector : TextureButton
{
	ItemChoice selection;
    TextureRect parentItem;
    int index;

	Texture2D selected;
	public Texture2D unselected;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		selection = GetNode<ItemChoice>("../..");

        parentItem = GetNode<TextureRect>("..");
        index = (parentItem.Name.ToString()[4]) - 48;

		selected = ResourceLoader.Load<Texture2D>("res://Textures/item_hover.png");
        unselected = ResourceLoader.Load<Texture2D>("res://Textures/empty_item_slot.png");

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(IsHovered()) 
		{
			selection.SetDescriptionText(selection.available_items[index].name + "\n\n" +
				selection.available_items[index].description);
		}
	}

    public override void _Pressed()
    {
		bool is_selected = selection.ToggleItem(index);

		this.TextureNormal = is_selected ? selected : unselected;

        base._Pressed();
    }
}
