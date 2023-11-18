using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class RestartButton : TextureButton
{
	LevelManager levelManager;

	Inventory inventory;

	GameOverScreen gameOverScreen;

	ItemChoice itemChoice;

	CurrencyCounter currencyCounter;

	CharacterBody2D characterBody;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		levelManager = GetNode<LevelManager>("../../LevelManager");
		inventory = GetNode<Inventory>("../../Inventory");
		gameOverScreen = GetNode<GameOverScreen>("../../GameOverScreen");
		itemChoice = GetNode<ItemChoice>("../../ItemSelection");
		currencyCounter = GetNode<CurrencyCounter>("../../CurrencyCounter");
		characterBody = GetNode<CharacterBody2D>("../../ProtagBody");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Pressed()
    {
		levelManager.current_level.Position = new Vector2(-1400, 800);
		levelManager.current_level = levelManager.beginning;
		levelManager.current_handler = (level_handler) levelManager.current_level;
        levelManager.current_level.Position = new Vector2(0, 0);

        levelManager.is_game_over = false;
		levelManager.is_changing_level = false;
		levelManager.is_beginning = true;
		levelManager.level_number = 0;

		levelManager.clearAllEnemies();

		inventory.current_item = -1;
        inventory.items = new Item[6];
		inventory.Visible = false;

		itemChoice.Visible = true;
		itemChoice.has_selected_items = false;
		itemChoice.selected_items = new List<Item>();
		itemChoice.PopulateSelection();

		currencyCounter.currencyAmount = 7000;
		currencyCounter.updateCounter((int)currencyCounter.currencyAmount);

		characterBody.Position = new Vector2(64, 608);

        gameOverScreen.is_game_over = false;
		gameOverScreen.screen_opacity = 0;
		gameOverScreen.Visible = false;

        Godot.Collections.Array<Node> selectorItems = itemChoice.GetChildren();

        for (int i = 0; i < 10; ++i) 
		{
			TextureButton currentButton = (TextureButton) selectorItems[i].GetNode("./ItemSelector");

			currentButton.TextureNormal = ((ChoiceSelector)currentButton).unselected;
		}

        base._Pressed();
    }
}
