using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class EventBus : Node
{
    [Signal]
    public delegate void GameStartedEventHandler();
    [Signal]
    public delegate void TypeSelectedEventHandler(string type);
	[Signal]
    public delegate void LoadTextEventHandler(String[] text);
    [Signal]
    public delegate void UnloadTextEventHandler();
    [Signal]
    public delegate void StartBattleEventHandler(Stats enemyStats, Stats playerStats);
    [Signal]
    public delegate void PlayerTurnStartedEventHandler(Moveset moveset);
    [Signal]
    public delegate void TurnSkippedEventHandler();
    [Signal]
    public delegate void ActionSelectedEventHandler(int button);
    [Signal]
    public delegate void PlayerTurnEndedEventHandler();
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
    [Signal]
    public delegate void SPReplenishedEventHandler();
    [Signal]
    public delegate void ItemObtainedEventHandler(int itemType);
    [Signal]
    public delegate void ItemSelectedEventHandler(int slotNum);
    [Signal]
    public delegate void HealthItemUsedEventHandler(int healthUpdate);
    [Signal]
    public delegate void SPItemUsedEventHandler(int spUpdate);
     
}
