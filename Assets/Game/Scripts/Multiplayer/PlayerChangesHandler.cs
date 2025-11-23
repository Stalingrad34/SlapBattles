using System;
using System.Collections.Generic;
using System.Linq;
using Colyseus.Schema;
using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Move.Components;
using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Multiplayer
{
  public class PlayerChangesHandler
  {
    private readonly string _playerId;
    private readonly Player _player;
    private readonly StateCallbackStrategy<State> _callbackStrategy;
    private readonly List<Action> _callbacks = new ();

    public PlayerChangesHandler(string playerId, Player player, StateCallbackStrategy<State> callbackStrategy)
    {
      _playerId = playerId;
      _player = player;
      _callbackStrategy = callbackStrategy;
      Subscribe();
    }

    private void Subscribe()
    {
      _callbacks.Add(_callbackStrategy.Listen(_player, p => p.position, OnPositionChanged, false));
      _callbacks.Add(_callbackStrategy.Listen(_player, p => p.rotationY, OnRotationChanged, false));
    }

    private void OnPositionChanged(Vector2Float oldPosition, Vector2Float newPosition)
    {
      if (newPosition == null)
      {
        return;
      }
      
      ref var changes = ref WorldHandler.GetWorld().NewEntity().Get<ServerPositionChanges>();
      changes.Id = _playerId;
      changes.Position = newPosition;
    }
    
    private void OnRotationChanged(float oldRotation, float newRotation)
    {
      ref var changes = ref WorldHandler.GetWorld().NewEntity().Get<ServerRotationChanges>();
      changes.Id = _playerId;
      changes.RotationY = newRotation;
    }
    
    public void Dispose()
    {
      _callbacks.ForEach(callback => callback?.Invoke());
    }
  }
}