using Godot;
using System;

public partial class Inventory : TextureRect
{
	Item[] items;

	const int item_slots = 6;

	CurrencyCounter counter;

	public int current_item = -1;

	RichTextLabel description;

	TextureRect[] item_icons;

	protag_movement movementScript;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		items = new Item[item_slots];
		counter = GetNode<CurrencyCounter>("../CurrencyCounter");
		description = GetNode<RichTextLabel>("./ItemDescription");

		item_icons = new TextureRect[item_slots];

		movementScript = GetNode<protag_movement>("../ProtagBody");

		for (int i = 0; i < item_slots; i++)
		{
			item_icons[i] = GetNode<TextureRect>("./Item" + i.ToString());
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		bool inv_is_pressed = Input.IsActionJustPressed("inv_toggle");

		if (inv_is_pressed) 
		{
			Visible = !Visible;
		}

		if(!Visible) 
		{
			current_item = -1;
			description.Text = string.Empty;
		}
	}

	public void setSelected(int index)
	{
        if ((!(index >= 0 && index < item_slots)) || (items[index] is null))
        {
            return;
        }

        current_item = index;
		description.Text = items[index].name + "\n\n" + items[index].description;
	}

	public void setItem(int index, Item item)
	{
		if (!(index >= 0 && index < item_slots))
		{
			return;
		}

		items[index] = item;
	}

	public void useItem(int index) 
	{
        if (!(index >= 0 && index < item_slots))
        {
            return;
        }

		Item current_item = items[index];

        items[index] = null;

        setItemTexture(index);
        this.current_item = -1;
        description.Text = string.Empty;

		for (int i = 0; i < current_item.modifiers.Length; ++i)
		{
			movementScript.modifierList.Add(current_item.modifiers[i]);
        }
		
    }

	public void sellItem(int index) 
	{
        if (!(index >= 0 && index < item_slots))
        {
            return;
        }

        counter.addCurrency(items[index].price);
        items[index] = null;

		setItemTexture(index);
        current_item = -1;
        description.Text = string.Empty;
    }

	public void setItemTexture(int index)
	{
        if (!(index >= 0 && index < item_slots))
        {
            return;
        }

		if (items[index] is null)
		{
			item_icons[index].Texture = null;
		}
		else 
		{
            item_icons[index].Texture = items[index].texture;

        }

    }
}
