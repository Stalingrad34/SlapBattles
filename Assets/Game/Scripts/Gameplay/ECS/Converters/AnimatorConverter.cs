using Game.Scripts.Gameplay.Data.Units;
using Game.Scripts.Gameplay.ECS.Common;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class AnimatorConverter : MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private UnitAnimator animator;
    
    public void Convert(EcsEntity entity)
    {
      entity.Get<AnimatorComponent>().UnitAnimator = animator;
    }
  }
}