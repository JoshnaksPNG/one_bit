using Godot;
using Godot.Collections;
using System;
using System.Runtime.CompilerServices;

public partial class AudioPlayer : AudioStreamPlayer2D
{
	Dictionary<string, AudioStream> music_library;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		music_library = new Dictionary<string, AudioStream>();

		music_library.Add("village", ResourceLoader.Load<AudioStream>("res://Audio/town_music.mp3"));
        music_library.Add("boss", ResourceLoader.Load<AudioStream>("res://Audio/boss_music.mp3"));
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void setPlay(bool isPlaying)
	{
		this.Playing = isPlaying;
	}

	public void setMusic(string music_name) 
	{
		this.Stream = music_library[music_name];
	}
}
