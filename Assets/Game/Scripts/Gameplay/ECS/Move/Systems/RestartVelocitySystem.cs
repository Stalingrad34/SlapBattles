using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Move.Systems
{
  public class RestartVelocitySystem : IEcsRunSystem
  {
    private EcsFilter<IdentifierComponent, TransformComponent, MoveVelocityComponent> _filter;
    private EcsFilter<RestartEvent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        var restartInfo = _eventFilter.Get1(i).RestartInfo;
        foreach (var ii in _filter)
        {
          if (_filter.Get1(ii).Id == restartInfo.id)
          {
            var position = new Vector3(restartInfo.x, 0, restartInfo.z);
            _filter.Get2(ii).Transform.position = position;
            _filter.Get3(ii).Velocity = Vector3.zero;
          }
        }
      }
    }
  }
}