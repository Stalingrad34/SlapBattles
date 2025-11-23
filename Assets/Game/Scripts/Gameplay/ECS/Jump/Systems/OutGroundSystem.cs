using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Jump.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Jump.Systems
{
  public class OutGroundSystem : IEcsRunSystem
  {
    private EcsFilter<JumpableComponent, OnCollisionExitEvent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        _eventFilter.Get1(i).IsFly = true;
      }
    }
  }
}