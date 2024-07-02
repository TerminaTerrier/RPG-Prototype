using Godot;
using System;

public partial class InspectArea : Area2D
{
	public IInspectable _inspectableObject;

   
    public bool DetectArea()
	{
		if(HasOverlappingAreas())
		{
           return true;
		}
		else
		{
			return false;
		}
	}

	public Area2D GetArea()
	{
		Godot.Collections.Array<Area2D> areas = GetOverlappingAreas();

        foreach(Area2D area in areas)
		{
            if(area is InspectArea)
			{
			    return area;
			}
			else
			{
				return null;
			}
		}
		
		return null;
	}

	public void TriggerInspection()
	{
		_inspectableObject.OutputInspectionResult();
	}

	public bool CheckInspectionStatus()
	{
		return _inspectableObject.GetInspectionStatus();
	}

	public void EndInspection()
	{
		_inspectableObject.EndInspection();
	}
}
