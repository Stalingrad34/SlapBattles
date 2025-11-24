using Game.Scripts.Gameplay.ECS.Dead.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Dead.Systems
{
  public class DestroyGameObjectSystem : IEcsRunSystem
  {
    private EcsFilter<GameObjectComponent> _gameObjectFilter;
    private EcsFilter<DestroyGameObjectEvent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        foreach (var ii in _gameObjectFilter)
        {
          if (_gameObjectFilter.Get1(ii).Id == _eventFilter.Get1(ii).Id)
          {
            Object.Destroy(_gameObjectFilter.Get1(ii).GameObject);
            _gameObjectFilter.GetEntity(ii).Get<DeadEvent>();
          }
        }
      }
    }
  }
}