using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Input.Components;
using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Move.Systems
{
  public class AxisMoveVelocitySystem : IEcsRunSystem
  {
    private EcsFilter<ControlComponent, TransformComponent, MoveVelocityComponent> _moveableFilter;
    
    public void Run()
    {
      foreach (var i in _moveableFilter)
      {
        var horizontal = _moveableFilter.Get1(i).HorizontalAxis;
        var vertical = _moveableFilter.Get1(i).VerticalAxis;
        var transform = _moveableFilter.Get2(i).Transform;
        var speed = _moveableFilter.Get3(i).Speed;
        
        _moveableFilter.Get3(i).Velocity = (transform.forward * vertical + transform.right * horizontal).normalized * speed;
      }
    }
  }
}