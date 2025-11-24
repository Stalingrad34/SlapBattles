using System;
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

namespace Game.Scripts.Infrastructure.States
{
  public class MainState : IEnterStateAsync, IExitState
  {
    private MultiplayerManager _multiplayer;
    private BattleGUIModel _guiModel;

    public async UniTask Enter()
    {
      SetCursorLocked(true);
      await SceneManager.LoadSceneAsync("Game/Scenes/Game");

      UIManager.SetCameraStack(Camera.main);

      _guiModel = new BattleGUIModel(this);
      UIManager.ShowGUI<BattleGUIView, BattleGUIModel>(_guiModel);

      WorldHandler.GetWorld().NewEntity().Get<GameSessionComponent>().MainState = this;

      _multiplayer = ServiceProvider.Get<MultiplayerManager>();
      _multiplayer.OnPlayerConnected += OnPlayerConnected;
      _multiplayer.OnPlayerDisconnected += OnPlayerDisconnected;
      _multiplayer.OnRestartMessageReceived += OnRestartMessageReceived;
      _multiplayer.OnStartSlapMessageReceived += OnStartSlapMessage;
      _multiplayer.OnSlapPunchMessageReceived += OnSlapPunchMessage;
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
      SetCursorLocked(true);
      _guiModel.SetRestartBtnActive(false);
      WorldHandler.GetWorld().NewEntity().Get<DestroyGameObjectEvent>().Id = restartInfo.playerId;
      OnPlayerConnected(restartInfo.playerId, restartInfo.player);
    }

    private void OnStartSlapMessage(string playerId)
    {
      WorldHandler.GetWorld().NewEntity().Get<StartSlapMessage>().PlayerId = playerId;
    }

    private void OnSlapPunchMessage(string slapMessage)
    {
      var slapInfo = JsonConvert.DeserializeObject<SlapPunchInfo>(slapMessage);
      WorldHandler.GetWorld().NewEntity().Get<SlapPunchMessage>().SlapInfo = slapInfo;
    }

    public void Exit()
    {
      _multiplayer.OnPlayerConnected -= OnPlayerConnected;
      _multiplayer.OnPlayerDisconnected -= OnPlayerDisconnected;
      _multiplayer.OnRestartMessageReceived -= OnRestartMessageReceived;
      _multiplayer.Disconnect();
    }

    public async UniTaskVoid Dead()
    {
      await UniTask.Delay(TimeSpan.FromSeconds(3));
      SetCursorLocked(false);
      _guiModel.SetRestartBtnActive(true);
    }

    public void Restart()
    {
      _multiplayer.SendRestartMessage();
    }

    private void SetCursorLocked(bool isLocked)
    {
      /*Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
      Cursor.visible = !isLocked;*/
    }
  }
}