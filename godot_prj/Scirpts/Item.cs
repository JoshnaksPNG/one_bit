using Godot;
using System;

public partial class Item : Node
{
	public float price;
	public string name;
	public string description;
	public string texturePath;
	public Texture2D texture;
	public PlayerStatModifier[] modifiers;

	public Item(string texture, string name, float price, string desc, PlayerStatModifier[] modifier_list)
	{
		this.price = price;
		this.texture = ResourceLoader.Load<Texture2D>(texture); 
		this.name = name;
		this.description = desc;

		this.modifiers = modifier_list;
	}
}
