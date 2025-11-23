using Game.Scripts.Gameplay.ECS.Common;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class RigidbodyConverter : MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private UnityEngine.Rigidbody rb;
    
    public void Convert(EcsEntity entity)
    {
      entity.Get<RigidbodyComponent>().Rigidbody = rb;
    }
  }
}