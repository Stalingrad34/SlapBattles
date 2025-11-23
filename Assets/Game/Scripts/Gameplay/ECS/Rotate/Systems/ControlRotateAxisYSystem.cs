using Game.Scripts.Gameplay.ECS.Input.Components;
using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Rotate.Systems
{
  public class ControlRotateAxisYSystem : IEcsRunSystem
  {
    private EcsFilter<ControlComponent, RotateAxisYComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        /*var minAngle = _filter.Get2(i).MinAngle;
        var maxAngle = _filter.Get2(i).MaxAngle;*/

        var angle = _filter.Get1(i).MouseHorizontal;
        var rotateY = _filter.Get2(i).Angle + angle;
        //rotateY = Mathf.Clamp(rotateY + angle, minAngle, maxAngle);
        
        _filter.Get2(i).Angle = rotateY;
      }
    }
  }
}