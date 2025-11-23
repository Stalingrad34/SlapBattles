using Game.Scripts.Gameplay.ECS.Dead.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Dead.Systems
{
  public class DestroySystem : IEcsRunSystem
  {
    private EcsFilter<DeadEvent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        _filter.GetEntity(i).Destroy();
      }
    }
  }
}