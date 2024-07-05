using Godot;
using System;

public partial class BattleManager : Node
{
	[Export]
	SceneData _sceneData;
	[Export]
	Camera2D _battleCamera;
	EventBus _eventBus;
	Stats _playerStats;
	Stats _enemyStats;
	BattlePlayer battlePlayer;
	BattleEnemy battleEnemy;
	BattleStatus battleStatus;
    
	enum BattleStatus
	{
		Active,
		Finished
	}

	public override void _Ready()
	{
		_eventBus = GetNode<EventBus>("/root/EventBus");
		
	}

	public void BattleStart()
	{
        battlePlayer = (BattlePlayer)_sceneData.BattlePlayer.Instantiate();
		battleEnemy = (BattleEnemy)_sceneData.BattleEnemy.Instantiate();
		AddChild(battlePlayer);
		AddChild(battleEnemy);

		_playerStats = battlePlayer.playerStats;
		_enemyStats = battleEnemy.enemyStats;
        
		_battleCamera.Enabled = true;
		battleStatus = BattleStatus.Active;
	}

	public void SetInstanceValues(InstanceStats playerInstanceData)
	{
         battlePlayer.SetPlayerInstanceValues(playerInstanceData);
	}

	public void ManageTurn()
	{
        
	}

	public void BattleStop()
	{

	}
}
