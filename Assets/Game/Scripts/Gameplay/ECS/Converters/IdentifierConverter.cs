using Game.Scripts.Gameplay.Data.Units;
using Game.Scripts.Gameplay.ECS.Common;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class IdentifierConverter : MonoBehaviour, IConvertToEntity, IUnitSetup
  {
    private string _id;
    
    public void Convert(EcsEntity entity)
    {
      entity.Get<IdentifierComponent>().Id = _id;
    }

    public void Setup(UnitData data)
    {
      _id = data.Id;
    }
  }
}