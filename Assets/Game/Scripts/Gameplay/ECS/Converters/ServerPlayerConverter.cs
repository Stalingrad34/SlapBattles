using Game.Scripts.Gameplay.ECS.Common;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class ServerPlayerConverter : MonoBehaviour, IConvertToEntity
  {
    public void Convert(EcsEntity entity)
    {
      entity.Get<ServerPlayerComponent>();
    }
  }
}