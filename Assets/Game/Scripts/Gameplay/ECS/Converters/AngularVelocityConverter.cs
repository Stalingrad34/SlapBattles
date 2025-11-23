using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class AngularVelocityConverter : MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private float speed;
    
    public void Convert(EcsEntity entity)
    {
      entity.Get<AngularVelocityComponent>().Speed = speed;
    }
  }
}