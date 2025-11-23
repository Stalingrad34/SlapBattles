using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Jump.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Jump.Systems
{
  public class CheckSphereGroundSystem : IEcsRunSystem
  {
    private EcsFilter<TransformComponent, JumpableComponent> _jumpableFilter;
    
    public void Run()
    {
      foreach (var i in _jumpableFilter)
      {
        var transform = _jumpableFilter.Get1(i).Transform;
        var checkRadius = _jumpableFilter.Get2(i).CheckSphereRadius;
        var checkMask = _jumpableFilter.Get2(i).CheckSphereMask;

        if (Physics.CheckSphere(transform.position, checkRadius, checkMask))
        {
          _jumpableFilter.Get2(i).IsFly = false;
          _jumpableFilter.Get2(i).LastChanceJumpTime = 0;
        }
        else
        {
          _jumpableFilter.Get2(i).LastChanceJumpTimer += Time.deltaTime;
          if (_jumpableFilter.Get2(i).LastChanceJumpTimer > _jumpableFilter.Get2(i).LastChanceJumpTime)
          {
            _jumpableFilter.Get2(i).IsFly = true;
          }
        }
      }
    }
  }
}