using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Rigidbody.Systems
{
  public class MoveVelocitySystem : IEcsRunSystem
  {
    private EcsFilter<RigidbodyComponent, MoveVelocityComponent> _moveableFilter;
    
    public void Run()
    {
      foreach (var i in _moveableFilter)
      {
        _moveableFilter.Get1(i).Rigidbody.linearVelocity = _moveableFilter.Get2(i).Velocity;
      }
    }
  }
}