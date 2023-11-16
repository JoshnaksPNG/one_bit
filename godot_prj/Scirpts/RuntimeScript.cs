using Godot;
using System;

public partial class RuntimeScript : Node2D
{
	const float resWidth = 1280;
	const float resHeight = 720;

	Inventory inventory;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		inventory = GetNode<Inventory>("./Inventory");
		inventory.setItem(0, new Item("res://Textures/heirloom_item.png", "MeeMaw's Favorite", 1000f, "Can be sold at any time.", new PlayerStatModifier[]
		{
			new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Currency, 0.1d, 1000f)
		}));

        inventory.setItem(1, new Item("res://Textures/strength_pill_item.png", "Ethical (TM) Strength Pills", 2000f, "Gives you the strength of another man for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Damage, 30d, 30f)
        }));

        inventory.setItem(2, new Item("res://Textures/sugar_item.png", "Pile of \"Sugar\"", 2000f, "Doubles your speed for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Speed, 30d, 300f)
        }));

        inventory.setItem(3, new Item("res://Textures/spring_shoe_item.png", "Spring-Soled Dress Shoes", 2000f, "Greatly increases your jump height for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.JumpVelocity, 30d, -300f)
        }));

        inventory.setItem(4, new Item("res://Textures/jetpack_item.png", "Faulty Jetpack", 2000f, "Increases your double-jump to the same height as your normal jump for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.DoubleJumpRatio, 30d, (2f / 5f))
        }));

        inventory.setItem(5, new Item("res://Textures/bolt_shorts_item.png", "Usain Bolt's Running Shorts", 2000f, "Doubles your dash speed for 30 seconds. (Dash Distance Remains The Same)", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.DashVelocity, 30d, 900f),
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.DashDuration, 30d, -0.15f)
        }));

        for (int i = 0; i < 6; i++)
		{
			inventory.setItemTexture(i);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Change Window Size when Updated
		Vector2I wSize = GetTree().Root.GetWindow().Size;
		float wWidth = wSize.X;
		float wHeight = wSize.Y;

		float ratioW = wWidth / resWidth;
		float ratioH = wHeight / resHeight;

		float scale = ratioW > ratioH ? ratioW : ratioH;

		GetTree().Root.ContentScaleFactor = scale;
	}
}
