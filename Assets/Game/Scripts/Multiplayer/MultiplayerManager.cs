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

namespace Game.Scripts.Multiplayer
{
  public class MultiplayerManager : ColyseusManager<MultiplayerManager>, IService
  {
    public event Action<string, Player> OnPlayerConnected;
    public event Action<string, Player> OnPlayerDisconnected;
    public event Action<string> OnStartSlapMessageReceived;
    public event Action<RestartInfo> OnRestartMessageReceived;
    
    private readonly Dictionary<string, PlayerChangesHandler> _changesHandlers = new();
    private ColyseusRoom<State> _room;
    private StateCallbackStrategy<State> _stateCallbackStrategy;
    private readonly List<Action> _callbacks = new ();

    public void Init()
    {
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
      _room.OnMessage<string>("startSlap", OnSlapMessage);
      _room.OnMessage<string>("Restart", OnRestartMessage);
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

    private void OnSlapMessage(string playerId)
    {
      OnStartSlapMessageReceived?.Invoke(playerId);
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

    public void SendShootMessage(SlapInfo slapInfo)
    {
      slapInfo.playerId = _room.SessionId;
      var json = JsonConvert.SerializeObject(slapInfo);
      SendMessage("shoot", json);
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
  }
}
