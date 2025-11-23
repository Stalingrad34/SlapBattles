using System.Collections.Generic;
using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.SendMessage.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class SendDataConverter : MonoBehaviour, IConvertToEntity
  {
    public void Convert(EcsEntity entity)
    {
      entity.Get<SendDataComponent>().SendData = new Dictionary<string, object>();
    }
  }
}