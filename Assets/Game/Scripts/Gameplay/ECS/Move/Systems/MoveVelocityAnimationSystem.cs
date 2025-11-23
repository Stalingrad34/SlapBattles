using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Move.Systems
{
  public class MoveVelocityAnimationSystem : IEcsRunSystem
  {
    private EcsFilter<TransformComponent, MoveVelocityComponent, AnimatorComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        var transform = _filter.Get1(i).Transform;
        var velocity = _filter.Get2(i).Velocity;
        var moveSpeed = _filter.Get2(i).Speed;

        var localVelocity = transform.InverseTransformVector(velocity);
        var speed = localVelocity.magnitude / moveSpeed;
        var sign = Mathf.Sign(localVelocity.z);
        
        _filter.Get3(i).UnitAnimator.SetMoveAnimation(speed * sign);
      }
    }
  }
}