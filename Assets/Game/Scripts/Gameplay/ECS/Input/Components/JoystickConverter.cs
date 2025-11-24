using Game.Scripts.Gameplay.Data;
using Game.Scripts.Infrastructure.Custom;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Input.Components
{
  public class JoystickConverter : MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private Joystick axisJoystick;
    [SerializeField] private RotateJoystick rotateJoystick;
    [SerializeField] private CustomButton slapButton;
    
    public void Convert(EcsEntity entity)
    {
      ref var component = ref entity.Get<JoystickComponent>();
      component.AxisJoystick = axisJoystick;
      component.RotateJoystick = rotateJoystick;
      component.SlapButton = slapButton;
    }
  }
}