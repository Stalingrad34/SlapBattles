using Game.Scripts.Gameplay.Data;
using Game.Scripts.Infrastructure.Custom;

namespace Game.Scripts.Gameplay.ECS.Input.Components
{
  public struct JoystickComponent
  {
    public Joystick AxisJoystick;
    public RotateJoystick RotateJoystick;
    public CustomButton SlapButton;
  }
}