using Godot;
using System;
using System.Diagnostics;

public partial class LevelManager : Node2D
{

	const int NEXTLVLX = 1320;

	const int CHARSTARTX = 64;

	const int BOSSOFFSET = 5;

	const int LVLTRANSITIONTIME = 3;

	Godot.Area2D level_trigger;

	Node2D[] normal_levels;

    Node2D[] boss_levels;

	Node2D village;

	Node2D beginning;

	Node2D current_level;
	level_handler current_handler; // Access Level Handler for current_level

	Node2D next_level;

	CharacterBody2D character;

	int level_number;

	public bool is_changing_level = false;

	public bool is_village;

	double lvl_change_velocity;
	double lvl_char_velocity;

    CurrencyCounter currency_counter;

	bool is_game_over = false;

    GameOverScreen game_over;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		level_number = 0;

		is_changing_level = false;

		level_trigger = GetNode<Godot.Area2D>("NextLevelTrigger");

		character = GetNode<CharacterBody2D>("../ProtagBody");

		beginning = GetNode<Node2D>("../BeginningLevel");

		village = GetNode<Node2D>("../VillageLevel");

        current_level = beginning;
		current_handler = (level_handler) current_level;

		normal_levels = new Node2D[]
		{
			GetNode<Node2D>("../BasicLevel"),
            GetNode<Node2D>("../BasicLevel2"),
            GetNode<Node2D>("../BasicLevel3")
        };

		boss_levels = new Node2D[]
		{
            GetNode<Node2D>("../EmptyLevel")
        };

        currency_counter = GetNode<CurrencyCounter>("../CurrencyCounter");

        game_over = GetNode<GameOverScreen>("../GameOverScreen");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!is_game_over) 
		{
            if (is_changing_level)
            {
                character.MoveLocalX((float)(lvl_char_velocity * delta));
                next_level.MoveLocalX((float)(lvl_change_velocity * delta));
                current_level.MoveLocalX((float)(lvl_change_velocity * delta));

                if (character.Position.X <= CHARSTARTX)
                {
                    character.Position = new Vector2(CHARSTARTX, character.Position.Y);
                }

                if (next_level.Position.X <= 0)
                {
                    next_level.Position = new Vector2(0, 0);
                }

                if (next_level.Position.X <= 0 && character.Position.X <= CHARSTARTX)
                {
                    current_level = next_level;
                    current_handler = (level_handler)current_level;

                    if (current_level != village)
                    {
                        current_handler.enemies = 1;
                        current_handler.displayDoors(true);
                    }
                    

                    is_changing_level = false;
                }
            }
            else
            {
                bool is_in_level_trigger = level_trigger.HasOverlappingBodies();

                if (is_in_level_trigger)
                {
                    is_changing_level = true;

                    // If Coming Out Of Boss Level (Entering Village)
                    if (level_number % BOSSOFFSET == 0 && level_number != 0 && !is_village)
                    {
                        next_level = village;
                        is_village = true;
                    }
                    else if (level_number % BOSSOFFSET == BOSSOFFSET - 1) // If Entering Boss Level
                    {
                        Random rng = new Random();
                        int level_index = rng.Next(boss_levels.Length);
                        next_level = boss_levels[level_index];
                        ++level_number;
                        is_village = false;
                    }
                    else // If Entering Normal Level
                    {
                        Random rng = new Random();

                        do
                        {
                            int level_index = rng.Next(normal_levels.Length);
                            next_level = normal_levels[level_index];
                        } while (next_level == current_level);

                        ++level_number;
                        is_village = false;
                    }

                    // Move Next Level To Position Ready To Transition
                    next_level.Position = new Vector2(NEXTLVLX, 0);

                    // Get Velocities To Change Levels (Pixels / Second)
                    lvl_change_velocity = (0d - (double)NEXTLVLX) / (double)LVLTRANSITIONTIME;
                    lvl_char_velocity = ((double)CHARSTARTX - (double)character.Position.X) / (double)LVLTRANSITIONTIME;
                }
                else
                {
                    if (currency_counter.currencyAmount <= 0)
                    {
                        game_over.TriggerGameOver(level_number);
                    }
                }
            }
        }
		
	}
}
