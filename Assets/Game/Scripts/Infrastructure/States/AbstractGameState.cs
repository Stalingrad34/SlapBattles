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
using Voody.UniLeo;

namespace Game.Scripts.Infrastructure.States
{
  public abstract class AbstractGameState: IEnterStateAsync, IExitState
  {
    protected MultiplayerManager Multiplayer;
    protected BattleGUIModel GuiModel;

    protected abstract UniTask LoadScene();

    public async UniTask Enter()
    {
      SetCursorLocked(true);
      await LoadScene();

      UIManager.SetCameraStack(Camera.main);

      GuiModel = new BattleGUIModel(this);
      UIManager.ShowGUI<BattleGUIView, BattleGUIModel>(GuiModel);

      WorldHandler.GetWorld().NewEntity().Get<GameSessionComponent>().GameState = this;

      Multiplayer = ServiceProvider.Get<MultiplayerManager>();
      Multiplayer.OnPlayerConnected += OnPlayerConnected;
      Multiplayer.OnPlayerDisconnected += OnPlayerDisconnected;
      Multiplayer.OnRestartMessageReceived += OnRestartMessageReceived;
      Multiplayer.OnStartSlapMessageReceived += OnStartSlapMessage;
      Multiplayer.OnSlapPunchMessageReceived += OnSlapPunchMessage;
      Multiplayer.Connect(AssetProvider.GetPlayerData()).Forget();
    }

    private void OnPlayerConnected(string key, Player player)
    {
      if (Multiplayer.IsPlayer(key))
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
      WorldHandler.GetWorld().NewEntity().Get<DisconnectDestroyEvent>().Id = key;
    }

    private void OnRestartMessageReceived(RestartInfo restartInfo)
    {
      SetCursorLocked(true);
      GuiModel.SetRestartBtnActive(false);
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
      Multiplayer.OnPlayerConnected -= OnPlayerConnected;
      Multiplayer.OnPlayerDisconnected -= OnPlayerDisconnected;
      Multiplayer.OnRestartMessageReceived -= OnRestartMessageReceived;
      Multiplayer.Disconnect();
    }

    public async UniTaskVoid Dead(Transform transform)
    {
      await UniTask.Delay(TimeSpan.FromSeconds(3));
      SetCursorLocked(false);
      //_guiModel.SetRestartBtnActive(true);
      var position = new Vector2Float() {x = transform.position.x, z = transform.position.z};
      var rotation = transform.rotation.eulerAngles.y;
      Multiplayer.SendRestartMessage(position, rotation);
    }

    public void Restart()
    {
      //_multiplayer.SendRestartMessage();
    }

    private void SetCursorLocked(bool isLocked)
    {
#if UNITY_EDITOR
      Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
      Cursor.visible = !isLocked;
#endif
    }
  }
}