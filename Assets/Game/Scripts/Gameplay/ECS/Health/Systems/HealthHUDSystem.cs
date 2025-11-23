using Game.Scripts.Gameplay.ECS.Health.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Health.Systems
{
  public class HealthHUDSystem : IEcsRunSystem
  {
    private EcsFilter<HealthComponent> _healthFilter;
    private Camera _mainCamera;
    
    public void Run()
    {
      foreach (var i in _healthFilter)
      {
        var current = _healthFilter.Get1(i).CurrentHealth;
        var max  = _healthFilter.Get1(i).MaxHealth;
        _healthFilter.Get1(i).ProgressBar.fillAmount = (float)current / max;
        
        if(_healthFilter.Get1(i).LookAt) 
          _healthFilter.Get1(i).ProgressBar.transform.LookAt(_mainCamera.transform);
      }
    }
  }
}