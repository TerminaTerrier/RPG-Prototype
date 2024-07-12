using Godot;
using System;

public partial class ActionContext : Node
{
	public IAction Action{ get; private set; }


	public void SetAction(IAction action)
	{
        Action = action;
	}

	public void EnactAction()
	{
        Action.Enact();
	}
	
}
