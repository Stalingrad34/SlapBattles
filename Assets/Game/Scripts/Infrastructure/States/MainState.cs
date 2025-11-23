using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Slap.Components;
using Game.Scripts.Gameplay.ECS.Spawn.Components;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Multiplayer;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;
using Voody.UniLeo;

namespace Game.Scripts.Infrastructure.States
{
  public class MainState : IEnterStateAsync, IExitState
  {
    private MultiplayerManager _multiplayer;

    public async UniTask Enter()
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      await SceneManager.LoadSceneAsync("Game/Scenes/Game");

      _multiplayer = ServiceProvider.Get<MultiplayerManager>();
      _multiplayer.OnPlayerConnected += OnPlayerConnected;
      _multiplayer.OnPlayerDisconnected += OnPlayerDisconnected;
      _multiplayer.OnRestartMessageReceived += OnRestartMessageReceived;
      _multiplayer.OnStartSlapMessageReceived += OnStartSlapMessage;
      _multiplayer.Connect(AssetProvider.GetPlayerData()).Forget();
    }

    private void OnPlayerConnected(string key, Player player)
    {
      if (_multiplayer.IsPlayer(key))
      {
        ref var spawnEvent = ref WorldHandler.GetWorld().NewEntity().Get<SpawnPlayerEvent>();
        spawnEvent.Id = key;
        spawnEvent.Position = player.position.ToVector3();
        spawnEvent.Player = player;
      }
      else
      {
        ref var spawnEvent = ref WorldHandler.GetWorld().NewEntity().Get<SpawnEnemyEvent>();
        spawnEvent.Id = key;
        spawnEvent.Position = player.position.ToVector3();
        spawnEvent.Player = player;
      }
    }

    private void OnPlayerDisconnected(string key, Player player)
    {
      
    }
    
    private void OnRestartMessageReceived(RestartInfo restartInfo)
    {
      WorldHandler.GetWorld().NewEntity().Get<RestartEvent>().RestartInfo = restartInfo;
    }

    private void OnStartSlapMessage(string playerId)
    {
      WorldHandler.GetWorld().NewEntity().Get<StartSlapMessage>().PlayerId = playerId;
    }

    public void Exit()
    {
      _multiplayer.OnPlayerConnected -= OnPlayerConnected;
      _multiplayer.OnPlayerDisconnected -= OnPlayerDisconnected;
      _multiplayer.OnRestartMessageReceived -= OnRestartMessageReceived;
      _multiplayer.Disconnect();
    }
  }
}