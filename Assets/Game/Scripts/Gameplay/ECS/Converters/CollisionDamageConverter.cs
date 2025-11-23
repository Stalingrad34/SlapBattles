using Game.Scripts.Gameplay.ECS.Damage.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class CollisionDamageConverter : MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private int damage;
    
    public void Convert(EcsEntity entity)
    {
      entity.Get<CollisionDamageComponent>().Damage = damage;
    }
  }
}