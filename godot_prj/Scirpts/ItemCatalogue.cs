using Godot;
using System;

public partial class ItemCatalogue : Node
{
	public static Item[] all_items = new Item[] 
	{
        new Item("res://Textures/heirloom_item.png", "MeeMaw's Favorite", 2000f, "Can be sold at any time.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Currency, 0.00000001d, 2000f)
        }),

        new Item("res://Textures/strength_pill_item.png", "Ethical (TM) Strength Pills", 1000f, "Gives you the strength of another man for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Damage, 30d, 30f)
        }),

        new Item("res://Textures/sugar_item.png", "Pile of \"Sugar\"", 1000f, "Doubles your speed for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Speed, 30d, 300f)
        }),

        new Item("res://Textures/spring_shoe_item.png", "Spring-Soled Dress Shoes", 1000f, "Greatly increases your jump height for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.JumpVelocity, 30d, -300f)
        }),

        new Item("res://Textures/jetpack_item.png", "Faulty Jetpack", 700f, "Increases your double-jump to the same height as your normal jump for 30 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.DoubleJumpRatio, 30d, (2f / 5f))
        }),

        new Item("res://Textures/bolt_shorts_item.png", "Usain Bolt's Running Shorts", 500f, "Doubles your dash speed for 30 seconds. (Dash Distance Remains The Same)", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.DashVelocity, 30d, 900f),
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.DashDuration, 30d, -0.15f)
        }),

        new Item("res://Textures/powerline_shoes_item.png", "Powerline Sneakers", 700f, "Increases your speed by 1.5 times the default speed for 50 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Speed, 50d, 150f)
        }),

        new Item("res://Textures/rabbit_foot_item.png", "Lucky Rabit's Foot", 500f, "Slightly increases your jump height for 50 seconds.", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.JumpVelocity, 50d, -200f)
        }),

        new Item("res://Textures/wallet_item.png", "Wallet You Found On The Ground", 500f, "Slowly recharges your currency over 15 seconds", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Currency, 15d, 0.5f)
        }),

        new Item("res://Textures/lead_item.png", "Chunk Of Lead", 400f, "Decreases your jump height by half for 50 seconds", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.JumpVelocity, 10d, 100f)
        }),

        new Item("res://Textures/brew_item.png", "Mysterious Brew", 900f, "Reduces your speed by half for 30 seconds, but tripples your strength for 40 seconds", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Speed, 30d, -150f),
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Damage, 30d, 60f)
        }),

        new Item("res://Textures/knife_item.png", "Mother's Kitchen Knife", 1200f, "You shouldn't be playing with this...", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Damage, 30d, 60f),
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.AttackSpeed, 30d, -0.13f)
        }),

        new Item("res://Textures/watch_item.png", "Grandfather's Pocketwatch", 500f, "Doubles your running and dashing speed for 10 seconds", new PlayerStatModifier[]
        {
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.Speed, 10d, 300f),
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.DashVelocity, 10d, 900f),
            new PlayerStatModifier(PlayerStatModifier.ModifiedStat.DashDuration, 10d, -0.15f)
        }),
    };
}
