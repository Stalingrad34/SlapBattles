using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Dead.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Dead.Systems
{
  public class DisconnectDestroySystem : IEcsRunSystem
  {
    private EcsFilter<IdentifierComponent, TransformComponent> _unitsFilter;
    private EcsFilter<DisconnectDestroyEvent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        foreach (var ii in _unitsFilter)
        {
          if (_unitsFilter.Get1(ii).Id != _eventFilter.Get1(i).Id)
            continue;
          
          Object.Destroy(_unitsFilter.Get2(ii).Transform.gameObject);
          _unitsFilter.GetEntity(ii).Destroy();
        }
      }
    }
  }
}