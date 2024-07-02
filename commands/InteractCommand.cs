using Godot;
using System;

public partial class InteractCommand : Node, ICommand
{
	IState _interactableObject;
	public InteractCommand(IState InteractableObject)
	{
		_interactableObject = InteractableObject;
	}
	public void Execute()
	{
		if(_interactableObject is IInteractable)
		{
            IInteractable interactableObject = (IInteractable)_interactableObject;
		    interactableObject.Interact();
		}
	}
}
