using Godot;
using System;

public partial class InspectingState : Node, IState, IMovable, IInteractable
{
    public ITransition FSM { get; set; }
	public InspectArea InspectArea{ get; set; }
	private bool _inspectionFinished;

    public InspectingState(ITransition fSM, InspectArea inspectArea)
    {
	    FSM = fSM;
		InspectArea = inspectArea;
    }

    public void Enter(Variant StateController)
    {
       if(InspectArea.DetectArea())
	   {
		   _inspectionFinished = false;
		   InspectArea otherArea = (InspectArea)InspectArea.GetArea();
		   otherArea.TriggerInspection();
	   }
	   else
	   {
		   FSM.TransitionState("StandingState");
	   }
    }

    public void Exit(Variant StateController)
    {
        
    }

	public void Move(Vector2 direction)
	{
		if(_inspectionFinished == true)
		{
		    FSM.TransitionState("WalkingState");
		}
	}

	public void Still(Vector2 direction)
	{
		if(_inspectionFinished == true)
		{
		    FSM.TransitionState("StandingState");
		}
	}

	public void Interact()
    {
        if(InspectArea.DetectArea())
	    {
		    InspectArea otherArea = (InspectArea)InspectArea.GetArea();
		    var status = otherArea.CheckInspectionStatus();
            GD.Print(status);
		    if(status == true)
		    {
			    otherArea.EndInspection();
				_inspectionFinished = true;
		    }
		    else
		    {
			    otherArea.TriggerInspection();
		    }
	    }
    }

    public void Update(Variant StateController, float delta)
    {
        
    }
    
}
