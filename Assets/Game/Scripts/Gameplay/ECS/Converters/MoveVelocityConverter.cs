using Game.Scripts.Gameplay.Data.Units;
using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class MoveVelocityConverter : MonoBehaviour, IConvertToEntity, IUnitSetup
  {
    private float _speed;
    
    public void Convert(EcsEntity entity)
    {
      entity.Get<MoveVelocityComponent>().Speed = _speed;
    }

    public void Setup(UnitData data)
    {
      _speed = data.Speed;
    }
  }
}