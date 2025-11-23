using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Move.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Move.Systems
{
  public class MoveTowardsSystem : IEcsRunSystem
  {
    private EcsFilter<TransformComponent, MoveTowardsComponent> _moveableFilter;
    
    public void Run()
    {
      foreach (var i in _moveableFilter)
      {
        var transform = _moveableFilter.Get1(i).Transform;
        var destination = _moveableFilter.Get2(i).Destination;
        var speed = _moveableFilter.Get2(i).Speed;

        transform.position = Vector3.MoveTowards(
          transform.position,
          destination,
          speed * Time.deltaTime);
        /* velocityMagnitude = _moveableFilter.Get2(i).Velocity.magnitude;

        if (velocityMagnitude < 0.05f)
        {
          transform.position = destination;
        }
        else
        {
          transform.position = Vector3.MoveTowards(
            transform.position,
            destination,
            velocityMagnitude * Time.deltaTime
          );
        }*/
      }
    }
  }
}