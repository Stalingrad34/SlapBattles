using Game.Scripts.Gameplay.ECS.Input.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Input.Systems
{
  public class JoystickSystem : IEcsRunSystem
  {
    private EcsFilter<JoystickComponent> _joystickFilter;
    private EcsFilter<ControlComponent> _controlFilter;
    
    public void Run()
    {
      foreach (var i in _joystickFilter)
      {
        foreach (var ii in _controlFilter)
        {
          _controlFilter.Get1(ii).HorizontalAxis = _joystickFilter.Get1(i).AxisJoystick.Horizontal;
          _controlFilter.Get1(ii).VerticalAxis = _joystickFilter.Get1(i).AxisJoystick.Vertical;
          _controlFilter.Get1(ii).MouseHorizontal = _joystickFilter.Get1(i).RotateJoystick.delta.x;
          _controlFilter.Get1(ii).MouseVertical = _joystickFilter.Get1(i).RotateJoystick.delta.y;
          _controlFilter.Get1(ii).MouseLeftClicked = _joystickFilter.Get1(i).SlapButton.WasPressed;
        }
      }
    }
  }
}