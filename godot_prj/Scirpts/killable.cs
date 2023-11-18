using Godot;
using System;

public partial interface killable
{
    public void kill()
	{
		((Node)this).QueueFree();
	}

	public void damage(float damage)
	{

	}
}
