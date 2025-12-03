using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Colyseus;
using Colyseus.Schema;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.Data.Units;
using Game.Scripts.Infrastructure;
using Game.Scripts.Infrastructure.Services;
using Newtonsoft.Json;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Multiplayer
{
  public class MultiplayerManager : ColyseusManager<MultiplayerManager>, IService
  {
    private const string ServerIP = "194.226.139.113";
    private const string LocalIP = "localhost";
    
    public event Action<string, Player> OnPlayerConnected;
    public event Action<string, Player> OnPlayerDisconnected;

    [SerializeField] private bool isLocal;
    
    private ColyseusRoom<State> _room;
    private StateCallbackStrategy<State> _stateCallbackStrategy;
    private readonly List<Action> _callbacks = new ();
    private readonly Dictionary<string, PlayerChangesHandler> _changesHandlers = new();

    public void Init()
    {
      _colyseusSettings.colyseusServerAddress = isLocal ? LocalIP : ServerIP;
      
      Instance.InitializeClient();

#if UNITY_EDITOR
      ApplicationLifecycleProvider.ApplicationQuit += () => Disconnect().Forget();
#endif
    }

    public async UniTask<ColyseusRoom<State>> ConnectTest()
    {
      var data = new Dictionary<string, object>()
      {
        {"speed", 0},
        {"position", new Vector2Float()},
        {"rotation", 0}
      };
      return await Instance.client.JoinOrCreate<State>("lobby_room", data).AsUniTask();
    }

    public async UniTask<ColyseusRoom<State>> Connect(PlayerData playerData, string roomName, Vector2Float position, float rotation)
    {
      var data = new Dictionary<string, object>()
      {
        {"speed", playerData.Speed},
        {"position", position},
        {"rotation", rotation}
      };
      _room = await Instance.client.JoinOrCreate<State>(roomName, data).AsUniTask();

      _stateCallbackStrategy = Callbacks.Get(_room);
      _callbacks.Add(_stateCallbackStrategy.OnAdd(s => s.players, OnPlayerAdd));
      _callbacks.Add(_stateCallbackStrategy.OnRemove(s => s.players, OnPlayerRemove));
      
      return _room;
    }

    public async UniTask Disconnect()
    {
      if (_room == null)
        return;
      
      _callbacks.ForEach(c => c());
      _callbacks.Clear();
      
      try
      {
        await _room.Leave().AsUniTask();
        _room = null;
      }
      catch (Exception e)
      {
        Debug.LogWarning(e);
      }
      
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

    public void Send(string key, Dictionary<string, object> data)
    {
      data["playerId"] = _room?.SessionId;
      _room?.Send(key, data);
    }
    
    public void Send(string key)
    {
      _room?.Send(key, _room?.SessionId);
    }
    
    public void Send(string key, string json)
    {
      _room?.Send(key, json);
    }
  }
}
