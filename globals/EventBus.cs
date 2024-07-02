using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class EventBus : Node
{
	[Signal]
    public delegate void LoadTextEventHandler(String[] text);
    [Signal]
    public delegate void UnloadTextEventHandler();
}
