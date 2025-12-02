using System;
using System.Collections.Generic;
using Colyseus;
using Colyseus.Schema;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.Data.Units;
using Game.Scripts.Infrastructure;
using Game.Scripts.Infrastructure.Services;
using Newtonsoft.Json;
using Sirenix.Utilities;
using UnityEngine;

namespace Game.Scripts.Multiplayer
{
  public class MultiplayerManager : ColyseusManager<MultiplayerManager>, IService
  {
    private const string ServerIP = "194.226.139.113";
    private const string LocalIP = "localhost";
    
    public event Action<string, Player> OnPlayerConnected;
    public event Action<string, Player> OnPlayerDisconnected;
    public event Action<string> OnStartSlapMessageReceived;
    public event Action<string> OnSlapPunchMessageReceived;
    public event Action<RestartInfo> OnRestartMessageReceived;

    [SerializeField] private bool isLocal;
    
    private readonly Dictionary<string, PlayerChangesHandler> _changesHandlers = new();
    private ColyseusRoom<State> _room;
    private StateCallbackStrategy<State> _stateCallbackStrategy;
    private readonly List<Action> _callbacks = new ();

    public void Init()
    {
      _colyseusSettings.colyseusServerAddress = isLocal ? LocalIP : ServerIP;
      
      Instance.InitializeClient();

#if UNITY_EDITOR
      ApplicationLifecycleProvider.ApplicationQuit += Disconnect;
#endif
    }

    public async UniTaskVoid Connect(PlayerData playerData)
    {
      var data = new Dictionary<string, object>()
      {
        {"speed", playerData.Speed},
      };
      _room = await Instance.client.JoinOrCreate<State>("state_handler", data).AsUniTask();

      _stateCallbackStrategy = Callbacks.Get(_room);
      _callbacks.Add(_stateCallbackStrategy.OnAdd(s => s.players, OnPlayerAdd));
      _callbacks.Add(_stateCallbackStrategy.OnRemove(s => s.players, OnPlayerRemove));
      
      _room.OnStateChange += OnChange;
      _room.OnMessage<string>("startSlap", OnStartSlapMessage);
      _room.OnMessage<string>("slapPunch", OnSlapPunchMessage);
      _room.OnMessage<string>("restart", OnRestartMessage);
    }

    public void Disconnect()
    {
      if (_room == null)
        return;
      
      _callbacks.ForEach(c => c());
      _callbacks.Clear();
      _room.OnStateChange -= OnChange;
      _room.Leave(false);
      _changesHandlers.Values.ForEach(h => h.Dispose());
      _changesHandlers.Clear();
    }

    public bool IsPlayer(string key)
    {
      return key.Equals(_room?.SessionId);
    }

    private void OnPlayerAdd(string key, Player player)
    {
      var handler = new PlayerChangesHandler(key, player, _stateCallbackStrategy);
      _changesHandlers.Add(key, handler);
      OnPlayerConnected?.Invoke(key, player);
    }
    
    private void OnPlayerRemove(string key, Player player)
    {
      _changesHandlers.Remove(key);
      OnPlayerDisconnected?.Invoke(key, player);
    }

    private void OnChange(State state, bool isFirstState)
    {
      
    }

    private void OnStartSlapMessage(string playerId)
    {
      OnStartSlapMessageReceived?.Invoke(playerId);
    }
    
    private void OnSlapPunchMessage(string data)
    {
      OnSlapPunchMessageReceived?.Invoke(data);
    }
    
    private void OnRestartMessage(string message)
    {
      var restartInfo = JsonConvert.DeserializeObject<RestartInfo>(message);
      OnRestartMessageReceived?.Invoke(restartInfo);
    }

    public void SendMessage(string key, Dictionary<string, object> data)
    {
      _room.Send(key, data);
    }
    
    public void SendMessage(string key, string data)
    {
      _room.Send(key, data);
    }
    
    public void SendDamageMessage(string key, int damage)
    {
      var data = new Dictionary<string, object>()
      {
        {"id", key},
        {"value", damage}
      };
      
      SendMessage("damage", data);
    }

    public void SendStartSlapMessage()
    {
      SendMessage("startSlap", _room?.SessionId);
    }
    
    public void SendSlapPunchMessage(SlapPunchInfo slapInfo)
    {
      var json = JsonConvert.SerializeObject(slapInfo);
      SendMessage("slapPunch", json);
    }

    public void SendRestartMessage(Vector2Float position, float rotation)
    {
      var data = new Dictionary<string, object>
      {
        {"playerId", _room?.SessionId},
        {"position", position},
        {"rotation", rotation}
      };
      SendMessage("restart", data);
    }
  }
}
