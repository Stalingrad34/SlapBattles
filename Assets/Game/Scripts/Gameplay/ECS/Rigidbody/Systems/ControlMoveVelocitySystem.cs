using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Input.Components;
using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Rigidbody.Systems
{
  public class ControlMoveVelocitySystem : IEcsRunSystem
  {
    private EcsFilter<ControlComponent, RigidbodyComponent, MoveVelocityComponent> _moveableFilter;
    
    public void Run()
    {
      foreach (var i in _moveableFilter)
      {
        var velocity = _moveableFilter.Get3(i).Velocity;
        velocity.y = _moveableFilter.Get2(i).Rigidbody.linearVelocity.y;
        _moveableFilter.Get2(i).Rigidbody.linearVelocity = velocity;
      }
    }
  }
}