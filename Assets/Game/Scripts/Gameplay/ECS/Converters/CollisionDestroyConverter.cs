using Game.Scripts.Gameplay.ECS.Destroy.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class CollisionDestroyConverter : MonoBehaviour, IConvertToEntity
  {
    public void Convert(EcsEntity entity)
    {
      entity.Get<CollisionDestroyComponent>();
    }
  }
}