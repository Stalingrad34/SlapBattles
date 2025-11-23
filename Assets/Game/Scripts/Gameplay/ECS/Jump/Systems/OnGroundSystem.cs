using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Jump.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Jump.Systems
{
  public class OnGroundSystem : IEcsRunSystem
  {
    private EcsFilter<JumpableComponent, OnCollisionStayEvent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        _eventFilter.Get1(i).IsFly = false;
      }
    }
  }
}