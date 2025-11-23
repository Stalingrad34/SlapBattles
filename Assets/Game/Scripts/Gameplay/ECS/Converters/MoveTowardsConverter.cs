using Game.Scripts.Gameplay.ECS.Move.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class MoveTowardsConverter : MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private float speed;
    
    public void Convert(EcsEntity entity)
    {
      entity.Get<MoveTowardsComponent>().Destination = transform.position;
      entity.Get<MoveTowardsComponent>().Speed = speed;
    }
  }
}