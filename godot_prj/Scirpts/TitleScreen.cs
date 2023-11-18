using Godot;
using System;

public partial class TitleScreen : TextureRect
{
    double display_time = 3d;
    double opacity_rate = 0.75d;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (Visible)
        {
            if(display_time > 0) 
            {
                display_time -= delta;
            } else 
            {
                Color modulate = Modulate;

                if (modulate.A > 0)
                {
                    modulate.A -= (float)(opacity_rate * delta);
                }

                if (modulate.A <= 0f)
                {
                    Visible = false;
                }

                Modulate = modulate;
            }
            
        }
    }
}
