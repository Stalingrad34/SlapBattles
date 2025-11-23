using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Rigidbody.Systems
{
  public class ForceSystem : IEcsRunSystem
  {
    private EcsFilter<RigidbodyComponent, AddForceEvent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        _eventFilter.Get1(i).Rigidbody.AddForce(_eventFilter.Get2(i).Force, ForceMode.Force);
        _eventFilter.GetEntity(i).Destroy();
      }
    }
  }
}