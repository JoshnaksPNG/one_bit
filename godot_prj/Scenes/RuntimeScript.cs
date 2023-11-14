using Godot;
using System;

public partial class RuntimeScript : Node2D
{
	const float resWidth = 1280;
	const float resHeight = 720;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

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
