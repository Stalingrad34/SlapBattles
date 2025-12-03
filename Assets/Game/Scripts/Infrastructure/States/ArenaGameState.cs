using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Dead.Components;
using Game.Scripts.Gameplay.ECS.Slap.Components;
using Game.Scripts.Gameplay.ECS.Spawn.Components;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Multiplayer;
using Game.Scripts.UI;
using Leopotam.Ecs;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using Voody.UniLeo;
using Random = UnityEngine.Random;

namespace Game.Scripts.Infrastructure.States
{
  public class ArenaGameState : AbstractGameState, IEnterStateAsync, IExitStateAsync
  {
    private MultiplayerManager _multiplayerManager;
    private BattleGUIModel _guiModel;

    public async UniTask Enter()
    {
      await SceneManager.LoadSceneAsync("Game/Scenes/ArenaGame");

      SetCursorLocked(true);
      SetCameraStack();

      _guiModel = new BattleGUIModel(this);
      UIManager.ShowGUI<BattleGUIView, BattleGUIModel>(_guiModel);

      WorldHandler.GetWorld().NewEntity().Get<ArenaGameStateComponent>().GameState = this;
      
      _multiplayerManager = ServiceProvider.Get<MultiplayerManager>();
      _multiplayerManager.OnPlayerConnected += SpawnPlayer;
      _multiplayerManager.OnPlayerDisconnected += DestroyPlayer;

      var playerData = AssetProvider.GetPlayerData();
      var spawnPosition = playerData.SpawnPoints[Random.Range(0, playerData.SpawnPoints.Count)];
      var position = new Vector2Float() {x = spawnPosition.x, z = spawnPosition.z};
      var rotation = spawnPosition.y;

      var room = await _multiplayerManager.Connect(playerData,"arena_room", position, rotation);
      
      room.OnMessage<string>("startSlap", OnStartSlapMessage);
      room.OnMessage<string>("slapPunch", OnSlapPunchMessage);
      room.OnMessage<string>("restart", OnRestartMessage);
    }
    
    public void LobbyTeleport()
    {
      StateMachine.EnterAsync<LobbyState>().Forget();
    }

    private void SpawnPlayer(string key, Player player)
    {
      var isPlayer = _multiplayerManager.IsPlayer(key);
      
      ref var spawnEvent = ref WorldHandler.GetWorld().NewEntity().Get<SpawnPotatoEvent>();
      spawnEvent.Id = key;
      spawnEvent.Position = player.position.ToVector3();
      spawnEvent.Rotation = Quaternion.Euler(0, player.rotationY, 0);
      spawnEvent.Player = player;
      spawnEvent.PrefabPath = isPlayer ? "HandPlayerPotato" : "HandEnemyPotato";
      spawnEvent.IsPlayer = isPlayer;
    }

    private void DestroyPlayer(string key, Player player)
    {
      WorldHandler.GetWorld().NewEntity().Get<DisconnectDestroyEvent>().Id = key;
    }

    public void SendStartSlapMessage()
    {
      _multiplayerManager.Send("startSlap");
    }
    
    private void OnStartSlapMessage(string playerId)
    {
      WorldHandler.GetWorld().NewEntity().Get<StartSlapMessage>().PlayerId = playerId;
    }
    
    public void SendSlapPunchMessage(SlapPunchInfo slapInfo)
    {
      var json = JsonConvert.SerializeObject(slapInfo);
      _multiplayerManager.Send("slapPunch", json);
    }
    
    private void OnSlapPunchMessage(string slapMessage)
    {
      var slapInfo = JsonConvert.DeserializeObject<SlapPunchInfo>(slapMessage);
      WorldHandler.GetWorld().NewEntity().Get<SlapPunchMessage>().SlapInfo = slapInfo;
    }

    public async UniTaskVoid SendRestartMessage(Transform transform)
    {
      await UniTask.Delay(TimeSpan.FromSeconds(3));
      SetCursorLocked(false);
      
      var data = new Dictionary<string, object>
      {
        {"position", new Vector2Float() {x = transform.position.x, z = transform.position.z}},
        {"rotation", transform.rotation.eulerAngles.y}
      };
      
      _multiplayerManager.Send("restart", data);
    }
    
    private void OnRestartMessage(string message)
    {
      var restartInfo = JsonConvert.DeserializeObject<RestartInfo>(message);
      SetCursorLocked(true);
      _guiModel.SetRestartBtnActive(false);
      WorldHandler.GetWorld().NewEntity().Get<DestroyGameObjectEvent>().Id = restartInfo.playerId;
      SpawnPlayer(restartInfo.playerId, restartInfo.player);
    }
    
    public async UniTask ExitAsync()
    {
      _multiplayerManager.OnPlayerConnected -= SpawnPlayer;
      _multiplayerManager.OnPlayerDisconnected -= DestroyPlayer;
      
      await _multiplayerManager.Disconnect();
    }
  }
}