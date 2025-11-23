using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Rotate.Converters
{
  public class RotateAxisXConverter : MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    
    public void Convert(EcsEntity entity)
    {
      ref var component = ref entity.Get<RotateAxisXComponent>();
      component.RotateSpeed = rotateSpeed;
      component.MinAngle = minAngle;
      component.MaxAngle = maxAngle;
    }
  }
}