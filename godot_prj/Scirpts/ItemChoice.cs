using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ItemChoice : TextureRect
{
	RichTextLabel description;

	public List<Item> selected_items;

	public Item[] available_items;

	TextureRect[] item_icons;

	Inventory inventory;

	public bool has_selected_items = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		description = GetNode<RichTextLabel>("./ItemDescription");
		selected_items = new List<Item>();
		available_items = new Item[10];

		inventory = GetNode<Inventory>("../Inventory");

		item_icons = new TextureRect[10];

		for (int i = 0; i < 10; ++i)
		{
			item_icons[i] = GetNode<TextureRect>("./Item" + i.ToString());
			
		}

		PopulateSelection();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(selected_items.Count >= 6 && !has_selected_items) 
		{
			for (int i = 0; i < 6; ++i)
			{
				inventory.setItem(i, selected_items[i]);
				inventory.setItemTexture(i);
			}

			this.Visible = false;
			has_selected_items = true;
		}
	}

	public void SetDescriptionText(string text)
	{
		description.Text = text;
	}

	public void AddItem(int index) 
	{
		selected_items.Add(available_items[index]);
	}

	public void RemoveItem(int index) 
	{
		selected_items.Remove(available_items[index]);
	}

	public bool ToggleItem(int index) 
	{
		Item indexed_item = available_items[index];

		if (selected_items.Contains(indexed_item))
		{
			selected_items.Remove(indexed_item);
			return false;
		}
		else
		{
			selected_items.Add(indexed_item);
			return true;
		}
	}

	public void PopulateSelection()
	{
		selected_items.Clear();

		Random rand = new Random();

		for(int i = 0; i <  available_items.Length; ++i) 
		{
			available_items[i] = ItemCatalogue.all_items[rand.Next(ItemCatalogue.all_items.Length)];
			setItemTexture(i);
		}
	}

    public void setItemTexture(int index)
    {
        if (!(index >= 0 && index < 10))
        {
            return;
        }

        item_icons[index].Texture = available_items[index].texture;
    }
}
