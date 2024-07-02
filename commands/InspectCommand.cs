using Godot;
using System;

public partial class InspectCommand : Node, ICommand
{
	IState _inspectableObject;
	public InspectCommand(IState InspectableObject)
	{
		_inspectableObject = InspectableObject;
	}
	public void Execute()
	{
		//Type-checking interfaces to prevent errors related to incompatible states.
		if(_inspectableObject is IInspectable)
		{
            IInspectable inspectableObject = (IInspectable)_inspectableObject;
		    inspectableObject.Inspect();
		}
	}
}
