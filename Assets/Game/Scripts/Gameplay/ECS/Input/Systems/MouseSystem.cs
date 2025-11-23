using Game.Scripts.Gameplay.ECS.Input.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Input.Systems
{
  public class MouseSystem : IEcsRunSystem
  {
    private EcsFilter<ControlComponent> _filter;
    
    public void Run()
    {
      var mouseX = UnityEngine.Input.GetAxis("Mouse X");
      var mouseY = UnityEngine.Input.GetAxis("Mouse Y");
      var isLeftClick = UnityEngine.Input.GetMouseButtonDown(0);
      
      foreach (var i in _filter)
      {
        _filter.Get1(i).MouseHorizontal = mouseX;
        _filter.Get1(i).MouseVertical = mouseY;
        _filter.Get1(i).MouseLeftClicked = isLeftClick;
      }
    }
  }
}