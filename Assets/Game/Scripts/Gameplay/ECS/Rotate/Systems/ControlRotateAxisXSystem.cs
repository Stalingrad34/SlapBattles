using Game.Scripts.Gameplay.ECS.Input.Components;
using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Rotate.Systems
{
  public struct ControlRotateAxisXSystem : IEcsRunSystem
  {
    private EcsFilter<ControlComponent, RotateAxisXComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        var minAngle = _filter.Get2(i).MinAngle;
        var maxAngle = _filter.Get2(i).MaxAngle;

        var angle = -_filter.Get1(i).MouseVertical;
        var rotateX = _filter.Get2(i).Angle;
        rotateX = Mathf.Clamp(rotateX + angle, minAngle, maxAngle);
        
        _filter.Get2(i).Angle = rotateX;
      }
    }
  }
}