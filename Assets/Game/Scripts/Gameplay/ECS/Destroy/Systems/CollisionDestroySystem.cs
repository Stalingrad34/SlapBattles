using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Destroy.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Destroy.Systems
{
  public class CollisionDestroySystem : IEcsRunSystem
  {
    private EcsFilter<TransformComponent, CollisionDestroyComponent, OnCollisionEnterEvent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        Object.Destroy(_filter.Get1(i).Transform.gameObject);
        _filter.GetEntity(i).Destroy();
      }
    }
  }
}