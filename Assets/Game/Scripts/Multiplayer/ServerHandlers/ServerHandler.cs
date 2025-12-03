using System;
using System.Collections.Generic;
using Colyseus;
using Colyseus.Schema;

namespace Game.Scripts.Multiplayer.ServerHandlers
{
  public abstract class ServerHandler
  {
    private ColyseusRoom<State> _room;
    private readonly Dictionary<string, PlayerChangesHandler> _changesHandlers = new();
    private StateCallbackStrategy<State> _stateCallbackStrategy;
    private readonly List<Action> _callbacks = new ();
    private readonly MultiplayerManager _multiplayerManager;

    protected ServerHandler(MultiplayerManager multiplayerManager)
    {
      _multiplayerManager = multiplayerManager;
    }
  }
}