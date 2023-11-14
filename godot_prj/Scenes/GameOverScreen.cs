using Godot;
using System;
using System.Diagnostics;

public partial class GameOverScreen : TextureRect
{
	public EndScore endScore;
	bool is_game_over = false;

	double screen_opacity = 0;
	double opacity_rate = 0.75d;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		endScore = GetNode<EndScore>("LevelScore");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (is_game_over) 
		{
            Color modulate = Modulate;

			if (modulate.A < 1)
			{
                modulate.A += (float)(opacity_rate * delta);
            }

			if (modulate.A > 1f) 
			{
				modulate.A = 1;
			}
            
            Modulate = modulate;
        }
	}

	public void TriggerGameOver(int score)
	{
		if(!is_game_over) 
		{
            Debug.Print("Triggered Game Over");
            this.Visible = true;

            is_game_over = true;
            endScore.updateCounter(score);

            Color modulate = Modulate;
            modulate.A = 0;
            Modulate = modulate;
        }
		
	}
}
