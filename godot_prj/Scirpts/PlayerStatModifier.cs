using Godot;
using System;

public partial class PlayerStatModifier : Node
{
	public ModifiedStat modifiedStat;
	public double duration;
	public float value;


	public PlayerStatModifier(ModifiedStat stat, double duration, float value) 
	{
		modifiedStat = stat;
		this.duration = duration;
		this.value = value;
	}

	public enum ModifiedStat 
	{
		Damage,
		Speed,
		JumpVelocity,
		DoubleJumpRatio,
		AttackSpeed,
		DashVelocity,
		DashDuration,
		Currency
	}
}
