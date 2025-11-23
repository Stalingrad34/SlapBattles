using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Rotate.Converters
{
  public class RotateAxisYConverter : MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private float rotateSpeed;
    
    public void Convert(EcsEntity entity)
    {
      entity.Get<RotateAxisYComponent>().RotateSpeed = rotateSpeed;
    }
  }
}