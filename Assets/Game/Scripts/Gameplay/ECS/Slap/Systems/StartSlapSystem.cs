using Game.Scripts.Gameplay.ECS.Slap.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Slap.Systems
{
  public class StartSlapSystem : IEcsRunSystem
  {
    private EcsFilter<SlapComponent, StartSlapEvent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        _filter.Get1(i).SlapView.StartSlapAnimation();
      }
    }
  }
}