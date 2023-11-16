using Godot;
using System;

public partial class ItemCatalogue : Node
{
	public static Item[] all_items = new Item[] 
	{
        new Item("res://Textures/heirloom_item.png", "MeeMaw's Favorite", 1000f, "Can be sold at any time.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Currency, 0.1d, 1000f)
        }),

        new Item("res://Textures/strength_pill_item.png", "Ethical (TM) Strength Pills", 2000f, "Gives you the strength of another man for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Damage, 30d, 30f)
        }),

        new Item("res://Textures/sugar_item.png", "Pile of \"Sugar\"", 2000f, "Doubles your speed for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Speed, 30d, 300f)
        }),

        new Item("res://Textures/spring_shoe_item.png", "Spring-Soled Dress Shoes", 2000f, "Greatly increases your jump height for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.JumpVelocity, 30d, -300f)
        }),

        new Item("res://Textures/jetpack_item.png", "Faulty Jetpack", 2000f, "Increases your double-jump to the same height as your normal jump for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.DoubleJumpRatio, 30d, (2f / 5f))
        }),

        new Item("res://Textures/bolt_shorts_item.png", "Usain Bolt's Running Shorts", 2000f, "Doubles your dash speed for 30 seconds. (Dash Distance Remains The Same)", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.DashVelocity, 30d, 900f),
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.DashDuration, 30d, -0.15f)
        }),
    };
}
