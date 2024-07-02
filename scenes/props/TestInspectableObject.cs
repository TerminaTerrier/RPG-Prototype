using Godot;
using System;

public partial class TestInspectableObject : Node2D, IInspectable
{
    [Export]
    FlavorText _flavorText;
    [Export]
    InspectArea inspectArea;
    EventBus eventBus;
    int _inspectionCounter;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
        inspectArea._inspectableObject = this;
    }

    public void Inspect()
    {
        
    }

    public void OutputInspectionResult()
    {
        eventBus.EmitSignal(EventBus.SignalName.LoadText, _flavorText._flavorText);
        _inspectionCounter++;
    }

    public bool GetInspectionStatus()
    {
        if(_inspectionCounter >= _flavorText._flavorText.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EndInspection()
    {
        _inspectionCounter = 0;
        eventBus.EmitSignal(EventBus.SignalName.UnloadText);
    }
}
