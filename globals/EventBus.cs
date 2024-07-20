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
    [Signal]
    public delegate void TurnEndedEventHandler();
    [Signal]
    public delegate void StatusChangedEventHandler(string status);
    [Signal]
    public delegate void PlayerHealthUpdatedEventHandler(int health);
    [Signal]
    public delegate void EnemyHealthUpdatedEventHandler(int health);
    [Signal]
    public delegate void PlayerSPUpdatedEventHandler(int sp);
    [Signal]
    public delegate void EnemySPUpdatedEventHandler(int sp);
    [Signal]
    public delegate void PlayerDeathEventHandler();
    [Signal]
    public delegate void EnemyDeathEventHandler();
    [Signal]
    public delegate void SPDepletedEventHandler(string entityName);
     
}
