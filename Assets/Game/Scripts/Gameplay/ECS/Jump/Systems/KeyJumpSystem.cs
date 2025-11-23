using Game.Scripts.Gameplay.ECS.Input.Components;
using Game.Scripts.Gameplay.ECS.Jump.Components;
using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Jump.Systems
{
  public class KeyJumpSystem : IEcsRunSystem
  {
    private EcsFilter<ControlComponent, JumpableComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        _filter.Get2(i).IntervalJumpTimer += Time.deltaTime;
          
        if (_filter.Get1(i).SpaceKeyDown && CanJump(_filter.Get2(i)))
        {
          _filter.GetEntity(i).Get<AddForceEvent>().Force = Vector3.up * _filter.Get2(i).JumpForce;
          _filter.Get2(i).IntervalJumpTimer = 0;
        }
      }
    }

    private bool CanJump(JumpableComponent jumpable)
    {
      var result = true;

      result &= !jumpable.IsFly;
      result &= jumpable.IntervalJumpTimer > jumpable.IntervalJumpTime;

      return result;
    }
  }
}