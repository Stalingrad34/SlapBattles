using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class MouseTransformRotateConverter: MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    
    public void Convert(EcsEntity entity)
    {
      entity.Get<MouseTransformRotateComponent>().Target = target;
      entity.Get<MouseTransformRotateComponent>().Speed = speed;
      entity.Get<MouseTransformRotateComponent>().MinAngle = minAngle;
      entity.Get<MouseTransformRotateComponent>().MaxAngle = maxAngle;
    }
  }
}