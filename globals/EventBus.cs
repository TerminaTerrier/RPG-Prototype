using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class EventBus : Node
{
	[Signal]
    public delegate void LoadTextEventHandler(String[] text);
    [Signal]
    public delegate void UnloadTextEventHandler();
    [Signal]
    public delegate void StartBattleEventHandler();
    [Signal]
    public delegate void PlayerTurnStartedEventHandler(Moveset moveset);
    [Signal]
    public delegate void ActionSelectedEventHandler(int button);
}
