using Game.Scripts.Gameplay.ECS.Destroy.Components;
using Game.Scripts.Gameplay.ECS.Health.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Health.Systems
{
  public class HealthDeadSystem : IEcsRunSystem
  {
    private EcsFilter<HealthComponent> _healthFilter;
    
    public void Run()
    {
      foreach (var i in _healthFilter)
      {
        if (_healthFilter.Get1(i).CurrentHealth <= 0)
        {
          _healthFilter.GetEntity(i).Get<DestroyEvent>();
        }
      }
    }
  }
}