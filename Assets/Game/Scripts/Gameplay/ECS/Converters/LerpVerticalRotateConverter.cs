using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class LerpVerticalRotateConverter : MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    
    public void Convert(EcsEntity entity)
    {
      entity.Get<LerpVerticalRotateComponent>().Target = target;
      entity.Get<LerpVerticalRotateComponent>().Speed = speed;
    }
  }
}