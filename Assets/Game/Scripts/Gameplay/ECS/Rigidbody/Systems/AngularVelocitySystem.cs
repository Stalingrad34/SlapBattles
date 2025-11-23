using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Rigidbody.Systems
{
  public class AngularVelocitySystem : IEcsRunSystem
  {
    private EcsFilter<RigidbodyComponent, AngularVelocityComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        
        _filter.Get1(i).Rigidbody.angularVelocity = _filter.Get2(i).AngularVelocity;
      }
    }
  }
}