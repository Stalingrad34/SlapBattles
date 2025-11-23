using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Rotate.Systems
{
  public class RotateAxisXSystem : IEcsRunSystem
  {
    private EcsFilter<TransformComponent, RotateAxisXComponent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        var currentRotation = _eventFilter.Get1(i).Transform.localEulerAngles;
        var rotation = new Vector3(_eventFilter.Get2(i).Angle, currentRotation.y, currentRotation.z);
        _eventFilter.Get1(i).Transform.localRotation = Quaternion.Lerp(_eventFilter.Get1(i).Transform.localRotation,
          Quaternion.Euler(rotation), _eventFilter.Get2(i).RotateSpeed * Time.deltaTime);
      }
    }
  }
}