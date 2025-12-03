using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Dead.Components;
using Game.Scripts.Gameplay.ECS.Spawn.Components;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Multiplayer;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;
using Voody.UniLeo;

namespace Game.Scripts.Infrastructure.States
{
  public class LobbyState : AbstractGameState, IEnterStateAsync, IExitStateAsync
  {
    private MultiplayerManager _multiplayerManager;

    public async UniTask Enter()
    {
      await SceneManager.LoadSceneAsync("Game/Scenes/Lobby");

      SetCursorLocked(true);
      SetCameraStack();

      WorldHandler.GetWorld().NewEntity().Get<LobbyGameStateComponent>().LobbyState = this;
      
      _multiplayerManager = ServiceProvider.Get<MultiplayerManager>();
      _multiplayerManager.OnPlayerConnected += SpawnPlayer;
      _multiplayerManager.OnPlayerDisconnected += DestroyPlayer;
      
      await _multiplayerManager.Connect(AssetProvider.GetPlayerData(),"lobby_room", new Vector2Float(), 0);
    }

    public void ArenaTeleport()
    {
      StateMachine.EnterAsync<ArenaGameState>().Forget();
    }

    private void SpawnPlayer(string key, Player player)
    {
      var isPlayer = _multiplayerManager.IsPlayer(key);
      
      ref var spawnEvent = ref WorldHandler.GetWorld().NewEntity().Get<SpawnPotatoEvent>();
      spawnEvent.Id = key;
      spawnEvent.Position = player.position.ToVector3();
      spawnEvent.Rotation = Quaternion.Euler(0, player.rotationY, 0);
      spawnEvent.Player = player;
      spawnEvent.PrefabPath = isPlayer ? "PlayerPotato" : "EnemyPotato";
      spawnEvent.IsPlayer = isPlayer;
    }

    private void DestroyPlayer(string key, Player player)
    {
      WorldHandler.GetWorld().NewEntity().Get<DisconnectDestroyEvent>().Id = key;
    }

    public async UniTask ExitAsync()
    {
      _multiplayerManager.OnPlayerConnected -= SpawnPlayer;
      _multiplayerManager.OnPlayerDisconnected -= DestroyPlayer;
      
      await _multiplayerManager.Disconnect();
    }
  }
}