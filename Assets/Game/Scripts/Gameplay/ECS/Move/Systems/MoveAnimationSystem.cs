using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Move.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Move.Systems
{
  public class MoveAnimationSystem : IEcsRunSystem
  {
    private EcsFilter<TransformComponent, MoveTowardsComponent, AnimatorComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        var position = _filter.Get1(i).Transform.position;
        var destination = _filter.Get2(i).Destination;
        var magnitude = (position - destination).magnitude;
        
        _filter.Get3(i).UnitAnimator.SetMoveAnimation(magnitude > 0.2f);
      }
    }
  }
}