using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Dead.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Dead.Systems
{
  public class AnimationDeadSystem : IEcsRunSystem
  {
    private EcsFilter<AnimatorComponent, DeadEvent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        _filter.Get1(i).UnitAnimator.SetDeadAnimation();
      }
    }
  }
}