using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class MouseAngularVelocityConverter: MonoBehaviour, IConvertToEntity
  {
    public void Convert(EcsEntity entity)
    {
      entity.Get<MouseAngularVelocityComponent>();
    }
  }
}