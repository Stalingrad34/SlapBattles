using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Rigidbody.Systems
{
  public class MouseAngularVelocitySystem : IEcsRunSystem
  {
    private EcsFilter<AngularVelocityComponent, MouseAngularVelocityComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        _filter.Get1(i).AngularVelocity = Vector3.up * _filter.Get2(i).MouseDelta * _filter.Get1(i).Speed;
        _filter.Get2(i).MouseDelta = 0;
      }
    }
  }
}