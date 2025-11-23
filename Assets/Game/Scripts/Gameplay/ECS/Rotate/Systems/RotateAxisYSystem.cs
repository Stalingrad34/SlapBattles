using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Rotate.Systems
{
  public class RotateAxisYSystem : IEcsRunSystem
  {
    private EcsFilter<TransformComponent, RotateAxisYComponent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        ref var transformComponent = ref _eventFilter.Get1(i);
        ref var rotateComponent = ref _eventFilter.Get2(i);
    
        var currentRotation = transformComponent.Transform.localEulerAngles;
    
        // Создаем целевой поворот
        var targetRotation = Quaternion.Euler(
          currentRotation.x, 
          rotateComponent.Angle, 
          currentRotation.z
        );
    
        // Плавный поворот
        transformComponent.Transform.localRotation = Quaternion.Lerp(
          transformComponent.Transform.localRotation,
          targetRotation,
          rotateComponent.RotateSpeed * Time.deltaTime
        );
      }
    }
  }
}