using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Jump.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Jump.Systems
{
  public class JumpAnimationSystem : IEcsRunSystem
  {
    private EcsFilter<AnimatorComponent, JumpableComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        _filter.Get1(i).UnitAnimator.SetFlyAnimation(!_filter.Get2(i).IsFly);
      }
    }
  }
}